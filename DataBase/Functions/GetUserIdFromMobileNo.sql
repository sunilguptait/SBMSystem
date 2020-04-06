USE [SBMS]
GO

/****** Object:  UserDefinedFunction [dbo].[GetUserIdFromMobileNo]    Script Date: 1/26/2020 12:53:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ASHOK KUMAWAT
-- Create date: 25-JAN-2020
-- Description:	GET GET USER ID DETAILS FROM MOBILE NO
-- =============================================
-- SELECT [DBO].GetUserIdFromMobileNo ('8058500526')
CREATE FUNCTION [dbo].[GetUserIdFromMobileNo]
(
	@MobileNo VARCHAR(20)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @UserId INT=0

	--IF EXISTS(SELECT 1 FROM UserLogin WHERE UL_LoginName=@MobileNo)
	--BEGIN
	--	SELECT @UserId=UL_Id FROM UserLogin WHERE UL_LoginName=@MobileNo
	--END
	IF EXISTS(SELECT 1 FROM Parents_Mas WHERE P_MobileNo=@MobileNo)
	BEGIN
		SELECT @UserId= P_Id FROM Parents_Mas WHERE P_MobileNo=@MobileNo
	END
	ELSE IF EXISTS(SELECT 1 FROM BookSeller_Mas WHERE BSM_MobileNo=@MobileNo)
	BEGIN
		SELECT @UserId= BSM_Id FROM BookSeller_Mas WHERE BSM_MobileNo=@MobileNo
	END
	ELSE IF EXISTS(SELECT 1 FROM Employee_Mas WHERE EMP_MobileNo=@MobileNo)
	BEGIN
		SELECT @UserId= EMP_Id FROM Employee_Mas WHERE EMP_MobileNo=@MobileNo
	END

	RETURN @UserId

END

GO


