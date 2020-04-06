USE [SBMS]
GO
IF OBJECT_ID ('GetInvoiceList') IS NOT NULL
   DROP PROCEDURE GetInvoiceList
GO
GO

/****** Object:  StoredProcedure [dbo].[GetInvoiceList]    Script Date: 1/18/2020 5:43:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--GetInvoiceList 0,'<ArrayOfString><string>0000007</string></ArrayOfString>',2
CREATE PROCEDURE [dbo].[GetInvoiceList]
	@BookSellerId INT,
	@OrderCodes XML='',
	@SessionId INT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Orders TABLE
	(
		OrderCode VARCHAR(100)
	)

    DECLARE @docHandle INT

	EXEC sp_xml_preparedocument @docHandle OUTPUT, @OrderCodes;  
	INSERT INTO @Orders(OrderCode)
	SELECT DISTINCT string FROM OPENXML(@docHandle, N'/ArrayOfString',2) 
	WITH 
	(
		string VARCHAR(100)
	) 
	AS exlLocation
	
	EXEC sp_xml_removedocument @docHandle;

	SELECT 
	OrderId					=	OM.Order_Id,
	OrderDate				=	CONVERT(VARCHAR(10),OM.Order_date,103),
	BookSellerFirmName		=	BS.BSM_FirmName ,
	BookSellerAddress1		=	BS.BSM_Address1,
	BookSellerAddress2		=	BS.BSM_Address2,
	BookSellerCity			=	CM.CityName,
	BookSellerState			=	SM.StateName,
	BookSellerPostCode		=	BS.BSM_PostCode,
	BookSellerEmail			=	BS.BSM_EmailId,
	BookSellerMobile		=	BS.BSM_MobileNo,
	StudentName				=	S.St_Name,
	StudentAddress			=	S.St_Address,
	ParentName				=	P.P_Name,
	ParentAddress1			=	P.P_Address1,
	ParentAddress2			=	P.P_Address2,
	ParentCity				=	CM1.CityName,
	ParentState				=	SM1.StateName,
	ParentPostCode			=	P.P_PostCode,
	ParentEmail				=	P.P_EmailId,
	ParentMobile			=	P.P_MobileNo,
	BookName				=	B.Book_Name,
	BookPrice				=	ISNULL(ODM.OD_Price,0),
	BookQty					=	ISNULL(ODM.OD_Qty,0),
	BookAmount				=	ISNULL(ODM.OD_Total,0),
	BookDiscount			=	ISNULL(ODM.OD_Discount,0),
	OrderAmount				=	ISNULL(OM.Order_TotalAmount,0),
	OrderCode				=	OM.Order_Code
	FROM Order_Mas OM
	INNER JOIN @Orders O ON O.OrderCode=OM.Order_Code
	INNER JOIN OrderDetail_Mas ODM ON ODM.OD_OrderId=OM.Order_Id
	INNER JOIN BookSeller_Mas BS ON BS.BSM_Id=OM.Order_BSId
	LEFT JOIN City_Mas CM ON CM.CityId=BS.BSM_CityId
	LEFT JOIN State_Mas SM ON SM.StateId=BS.BSM_StateId
	LEFT JOIN Student_Enrollment SE ON SE.SE_Id=OM.Order_SEId
	LEFT JOIN Student_Mas S ON S.St_id=SE.SE_StudentId
	LEFT JOIN Parents_Mas P ON P.P_Id=S.St_id
	LEFT JOIN City_Mas CM1 ON CM1.CityId=P.P_CityId
	LEFT JOIN State_Mas SM1 ON SM1.StateId=P.P_StateId
	LEFT JOIN Book_Mas B ON B.Book_Id=ODM.OD_BookId
	WHERE OM.Order_BSId=@BookSellerId AND ISNULL(ODM.OD_IsDelete,0)=0 --AND SE.SE_Id=@SessionId


END

GO


