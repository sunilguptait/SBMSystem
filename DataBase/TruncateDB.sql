USE [SBMS]

--DECLARE @nombre NVARCHAR(100)
--DECLARE @tablas TABLE(nombre nvarchar(100))

--INSERT INTO @tablas
--SELECT t.TABLE_SCHEMA+ '.'+t.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES T
--DECLARE @contador INT=0

--SELECT @contador=COUNT(*) FROM INFORMATION_SCHEMA.TABLES

--WHILE @contador>0 
--BEGIN
--    SELECT TOP 1 @nombre=nombre FROM @tablas 
--    DECLARE @sql NVARCHAR(500)=''
--    SET @sql =@sql+'Truncate table  '+@nombre
--    EXEC (@sql)
--    SELECT @sql
--    SET @contador=@contador-1   
--    DELETE TOP (1) @tablas 
--END

--SELECT CONCAT('DELETE FROM [',TABLE_NAME,']')
--FROM INFORMATION_SCHEMA.TABLES
--WHERE TABLE_CATALOG='SBMS'

--SELECT CONCAT('DBCC CHECKIDENT ([',TABLE_NAME,'], RESEED, 0)')
--FROM INFORMATION_SCHEMA.TABLES
--WHERE TABLE_CATALOG='SBMS'

--SELECT * FROM UserLogin
DELETE FROM UserLogin WHERE UL_Id>1

DELETE FROM [Order_Mas]
DELETE FROM [BooksClass_Mapping]
--DELETE FROM [EmailAccount]
DELETE FROM [BookSeller_Mas]
--DELETE FROM [BookType_Mas]
--DELETE FROM [City_Mas]
DELETE FROM [Delivery_Detail]
DELETE FROM [Employee_Mas]
DELETE FROM [OrderDetail_Mas]
DELETE FROM [Parents_Mas]
DELETE FROM [SellerSchool_Mapping]
DELETE FROM [Session_Mas]
--DELETE FROM [State_Mas]
DELETE FROM [Student_Enrollment]
--DELETE FROM [UserLogin]
DELETE FROM [School_Mas]
DELETE FROM [Class_Mas]
DELETE FROM [Publisher_Mas]
DELETE FROM [Book_Mas]
DELETE FROM [Student_Mas]

DBCC CHECKIDENT ([UserLogin], RESEED, 0)
DBCC CHECKIDENT ([Order_Mas], RESEED, 0)
DBCC CHECKIDENT ([BooksClass_Mapping], RESEED, 0)
DBCC CHECKIDENT ([EmailAccount], RESEED, 0)
DBCC CHECKIDENT ([BookSeller_Mas], RESEED, 0)
--DBCC CHECKIDENT ([BookType_Mas], RESEED, 0)
--DBCC CHECKIDENT ([City_Mas], RESEED, 0)
DBCC CHECKIDENT ([Delivery_Detail], RESEED, 0)
DBCC CHECKIDENT ([Employee_Mas], RESEED, 0)
DBCC CHECKIDENT ([OrderDetail_Mas], RESEED, 0)
DBCC CHECKIDENT ([Parents_Mas], RESEED, 0)
DBCC CHECKIDENT ([SellerSchool_Mapping], RESEED, 0)
DBCC CHECKIDENT ([Session_Mas], RESEED, 0)
--DBCC CHECKIDENT ([State_Mas], RESEED, 0)
DBCC CHECKIDENT ([Student_Enrollment], RESEED, 0)
--DBCC CHECKIDENT ([UserLogin], RESEED, 0)
DBCC CHECKIDENT ([School_Mas], RESEED, 0)
DBCC CHECKIDENT ([Class_Mas], RESEED, 0)
DBCC CHECKIDENT ([Publisher_Mas], RESEED, 0)
DBCC CHECKIDENT ([Book_Mas], RESEED, 0)
DBCC CHECKIDENT ([Student_Mas], RESEED, 0)

INSERT INTO Session_Mas (Session_Name,Session_ShortName,Session_IsCurrent)
SELECT '2019-2020','2019-2020',1
