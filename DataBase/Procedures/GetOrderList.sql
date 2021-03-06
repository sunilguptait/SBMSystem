USE [SBMS]
IF OBJECT_ID ('GetOrdersList') IS NOT NULL
   DROP PROCEDURE GetOrdersList
GO

GO
/****** Object:  StoredProcedure [dbo].[GetOrdersList]    Script Date: 1/12/2020 3:06:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ASHOK KUMAWAT
-- Create date: 11-JAN-2020
-- Description:	TO GET ORDERS LIST
-- =============================================
-- GetOrdersList @BookSellerId=2,@TotalRecords=0
CREATE PROCEDURE [dbo].[GetOrdersList]
	@BookSellerId INT,
	@DeliveryStatus INT=0,
	@PaymentStatus INT=0,
	@StudentName VARCHAR(50)='',
	@OrderNumber VARCHAR(10)='',
	@OrderDate DATE=NULL,
	@SessionId INT=0,

	@PageIndex INT =1,
	@PageSize INT=20,
	@OrderBy VARCHAR(20)='',
	@OrderDirection VARCHAR(10)='',
	@TotalRecords INT OUTPUT
	
AS
BEGIN

	SET NOCOUNT ON;
	--GET CURRENT SESSION
	IF(@SessionId=0)
	BEGIN
		SELECT @SessionId=Session_id FROM Session_Mas WHERE Session_IsCurrent=1
	END

	IF(ISNULL(@OrderBy,'')='')
	BEGIN
		SET @OrderBy='Order_date'
	END

	IF(ISNULL(@OrderDirection,'')='')
	BEGIN
		SET @OrderDirection='Desc'
	END

	SELECT OM.*,SM.St_Name,SM.St_id INTO #tmpOrderMaster FROM Order_Mas OM 
		INNER JOIN Student_Enrollment SE ON OM.Order_SEId=SE.SE_Id
		INNER JOIN Student_Mas SM ON SM.St_id=SE.SE_StudentId
		WHERE OM.Order_BSId=@BookSellerId
		AND SE.SE_SessionId=@SessionId
		AND (@DeliveryStatus=0 OR OM.Order_Status=@DeliveryStatus)
		AND (@PaymentStatus=-1 OR OM.Order_PaymentStatus=@PaymentStatus)
		AND (ISNULL(@OrderNumber,'')='' OR OM.Order_Code=@OrderNumber)
		AND (ISNULL(@StudentName,'')='' OR SM.St_Name=@StudentName)
		AND (ISNULL(@OrderDate,'')='' OR OM.Order_date=@OrderDate)

	SET @TotalRecords=@@ROWCOUNT
	DECLARE @Query VARCHAR(MAX)=''
	SET @Query=CONCAT('SELECT * FROM #tmpOrderMaster ORDER BY ',@OrderBy,' ',@OrderDirection,' OFFSET ',@PageIndex-1,'*',@PageSize,' ROWS FETCH NEXT ',@PageSize,' ROWS ONLY;')
	--print @Query
	EXEC(@Query)
END