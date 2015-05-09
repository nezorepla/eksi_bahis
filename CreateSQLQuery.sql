CREATE table EB_Dim_Yazar (
YID int identity(1,1),
YAZAR varchar(250) ,
SUSER bit );
INSERT INTO EB_Dim_Yazar (YAZAR,SUSER)VALUES ('SISTEM',1);

create table EB_Tbl_GirisLog(
INTCODE  int identity(1,1),
YID INT,
GIRIS DATETIME) ;
INSERT INTO EB_Tbl_GirisLog(YID) VALUES(1);

create table EB_Tbl_ParaLog(
INTCODE  int identity(1,1),
YID INT,
TUTAR FLOAT,
KAYNAKID INT,
ACIKLAMA VARCHAR(400),
LDT DATETIME)
 
create PROC EB_SP_YazarBakiye(@YID int)as
select SUM(TUTAR) BAKIYE 
FROM EB_Tbl_ParaLog
WHERE YID=@YID;
 
CREATE Proc EB_SP_YazarGiris(@User varchar(250),@yazarFlg BIT ) as
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