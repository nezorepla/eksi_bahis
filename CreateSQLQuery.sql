CREATE table EB_Dim_Yazar (
YID int identity(1,1),
YAZAR varchar(250) ,
SUSER bit );
INSERT INTO EB_Dim_Yazar (YAZAR,SUSER)VALUES ('SISTEM',1);
------------------------------------------
create table EB_Tbl_GirisLog(
INTCODE  int identity(1,1),
YID INT,
GIRIS DATETIME) ;
INSERT INTO EB_Tbl_GirisLog(YID) VALUES(1);
------------------------------------------
create table EB_Tbl_ParaLog(
INTCODE  int identity(1,1),
YID INT,
TUTAR FLOAT,
KAYNAKID INT,
ACIKLAMA VARCHAR(400),
LDT DATETIME)
 ------------------------------------------
 CREATE TABLE [dbo].[EB_Tmp_Dunun](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sira] [tinyint] NOT NULL,
	[adres] [varchar](255) NOT NULL,
	[baslik] [varchar](255) NOT NULL,
	[yazar] [varchar](255) NOT NULL,
	[tarih] [datetime] NULL
) ON [PRIMARY]
 
 
------------------------------------------

ALTER Proc [dbo].[EB_SP_YazarGiris](@User varchar(250),@yazarFlg BIT ) as
declare @YID int;
DECLARE @FLG BIT;
select @YID=YID,@FLG=SUSER from EB_Dim_Yazar
where YAZAR=@User;

if(isnull(@YID,0)=0)
BEGIN 
--ILK DEFA SAYFAYA GIRIYOR. 
--1 YAZAR YARATILIR
INSERT INTO EB_Dim_Yazar (YAZAR,SUSER)
VALUES (@User,@yazarFlg);


--YENI YARATILAN YAZARIN IDSI ALINIR
 select @YID=YID,@FLG=SUSER from EB_Dim_Yazar
where YAZAR=@User;

-- PARASI YATIRILIR.
INSERT INTO EB_Tbl_ParaLog (YID ,
TUTAR ,
KAYNAKID ,
ACIKLAMA,
LDT)
VALUES ( @YID,500,1,'ILK GIRIS',GETDATE());



END


--1 GIRIS LOGU EKLENIR
INSERT INTO EB_Tbl_GirisLog(YID)
VALUES(@YID);

select @YID YID,@FLG SUSER ;
------------------------------------------
CREATE TABLE [dbo].[EB_Dat_Dunun](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[eid] [bigint] NOT NULL,
	[sira] [tinyint] NOT NULL,
	[adres] [varchar](255) NOT NULL,
	[baslik] [varchar](255) NOT NULL,
	[yazar] [varchar](255) NOT NULL,
	[tarih] [datetime] NULL,
	[durum] [bit] NULL
) ON [PRIMARY]
------------------------------------------
CREATE TABLE [dbo].[EB_Def_Durum](
	[ICode] [int] IDENTITY(1,1) NOT NULL,
	[SCode] [tinyint] NULL,
	[SDef] [varchar](50) NULL,
	[SDt] [datetime] NULL
) ON [PRIMARY]

------------------------------------------

alter proc EB_SP_DEBE as
declare @a1 int;
declare @a2 int;

 select @a1 =  count(*)  from EB_Def_Durum where DATEDIFF(day,sdt,getdate())=0

 if (isnull(@a1,0)=0)
 begin
 select @a2 =  count(*)  from EB_Tmp_Dunun where DATEDIFF(day,tarih,getdate())=0
end


 if (isnull(@a1,0)=0 and isnull(@a2,0)>40 )
 begin

insert into EB_Def_Durum (SCode,SDef,SDt)
values(3,'DEBE',GETDATE());

insert into EB_Dat_Dunun
select RIGHT(adres,8) eid,sira, adres, baslik, yazar, tarih , 0 durum from dbo.EB_Tmp_Dunun
end
truncate table eb_Tmp_Dunun;
--truncate table EB_Def_Durum
------------------------------------------
alter PROC EB_SP_Dunun as
select cast(sira as varchar(2))sira ,'<a href="https://eksisozluk.com'+adres+'">'+baslik+'/#'+cast(eid as varchar(8))+'</a> ' entry, yazar
   from EB_dat_Dunun where DATEDIFF(day,tarih,getdate())=0

------------------------------------------
create PROC EB_SP_YazarBakiye(@YID int)as
select SUM(TUTAR) BAKIYE 
FROM EB_Tbl_ParaLog
WHERE YID=@YID;

 

 

------------------------------------------