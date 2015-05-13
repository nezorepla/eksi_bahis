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
create table EB_Tbl_Entrys (
IntCode int identity(1,1),
eid bigint,
baslik varchar(250),
entry varchar(max),
description varchar(max),
dt datetime,
yazar varchar(150),
favcnt int,
idt datetime)
------------------------------------------
alter proc [dbo].[EB_SP_EntryAdd] (@id bigint ,@baslik varchar(250),@en varchar(max),@ds varchar(max),@yazar varchar(150),@favcnt int,@tm varchar(200)) as

insert into EB_Tbl_Entrys(eid,baslik,entry,description,dt,yazar,favcnt,idt)
select @id ,@baslik, @en ,@ds, convert(datetime,@tm,104),@yazar,@favcnt,GETDATE()

-- 
 -- =============================================
create FUNCTION EB_Fx_YazarScore
( @yazar varchar(40)
)
RETURNS  float
AS
BEGIN

  declare @score float;


select @score=CONVERT(float,sum(case yazar when @yazar then adet else 1 end))/ CONVERT(float,sum(adet)) 
from (
SELECT yazar ,COUNT(*) adet
  FROM ( select distinct * from dbo.EB_hist_debe 
 )a  
 group by yazar
 having COUNT(*)>10
 )X
 
	-- Return the result of the function
	RETURN @score

END
GO

-- =============================================
create FUNCTION EB_Fx_ZamanScore
( @time datetime
)
RETURNS  float
AS
BEGIN

  declare @score float;
  declare @zaman datetime;

--select  @zaman=
--datepart(hour,dt)
--from EB_Tbl_Entrys
--where eid=51276861

select @score=CONVERT(float,sum(case dt when datepart(hour,@time) then adet else 1 end))/ CONVERT(float,sum(adet)) 
from (
SELECT datepart(hour,dt) dt ,COUNT(*) adet
  FROM ( select distinct * from dbo.EB_hist_debe 
 )a  
 group by datepart(hour,dt)
  )X
  
	-- Return the result of the function
	RETURN @score

END
GO

--------------------------------
-- =============================================
create FUNCTION EB_Fx_BaslikScore
( @baslik varchar(40)
)
RETURNS  float
AS
BEGIN

  declare @score float;
  
  select @score=CONVERT(float,sum(case baslik when @baslik then adet else 1 end))/ CONVERT(float,sum(adet)) 
from (
SELECT baslik ,COUNT(*) adet
  FROM ( select distinct * from dbo.EB_hist_debe 
 )a  
 group by baslik
   )X
  
	-- Return the result of the function
	RETURN @score

END
GO
-- =============================================
create FUNCTION EB_Fx_DebeOran
(@eid int
)
RETURNS  float
AS
BEGIN

  declare @score float;

select @score=dbo.EB_Fx_YazarScore(yazar) /dbo.EB_Fx_ZamanScore(dt) / dbo.EB_Fx_BaslikScore(baslik)
from EB_Tbl_Entrys
where eid=@eid  

  
	-- Return the result of the function
	RETURN ROUND(@score,2)

END
GO

creATE proc [dbo].[EB_SP_Bulten_Debe] as

select baslik,yazar,dt  ,DBO.EB_Fx_DebeOran(eid) DebeOran
from EB_Tbl_Entrys
where DATEDIFF(day,dateadd(hour,-6,GETDATE()),dt)=0