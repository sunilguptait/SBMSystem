USE [SBMS]
GO
IF OBJECT_ID ('ValidateUserOnParentsRegistration') IS NOT NULL
   DROP PROCEDURE ValidateUserOnParentsRegistration
GO
/****** Object:  StoredProcedure [dbo].[ValidateUserOnParentsRegistration]    Script Date: 1/18/2020 9:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ASHOK KUMAWAT
-- Create date: 15-JAN-2019
-- Description:	TO VALIDATE LOGIN NAME ON PARENTS REGISTRATION IN MOBILE APP
-- =============================================
CREATE PROCEDURE [dbo].[ValidateUserOnParentsRegistration] 
@LoginName VARCHAR(100),
@LoginType INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ReturnValue INT =0, @ReturnMessage VARCHAR(250)=''
    SET @ReturnValue=0 	
	IF EXISTS(SELECT 1 FROM BookSeller_Mas BM WITH(NOLOCK) WHERE LTRIM(RTRIM(@LoginName))=CASE WHEN @LoginType=1 THEN BSM_MobileNo ELSE BSM_EmailId END)
	BEGIN
		SET @ReturnValue=-1 -- Provided Login name is already Registered with another Login Type
		SET @ReturnMessage=CONCAT('Provided '+CASE WHEN @LoginType=1 THEN 'Mobile No' ELSE 'Email Id' END,' is already Registered with another Login Type')
	END
	ELSE IF EXISTS(SELECT 1 FROM Employee_Mas BM WITH(NOLOCK) WHERE LTRIM(RTRIM(@LoginName))=CASE WHEN @LoginType=1 THEN EMP_MobileNo ELSE EMP_EmailId END)
	BEGIN
		SET @ReturnValue=-2 -- Provided Login name is already Registered with another Login Type
		SET @ReturnMessage=CONCAT('Provided '+CASE WHEN @LoginType=1 THEN 'Mobile No' ELSE 'Email Id' END,' is already Registered with another Login Type')
	END
	ELSE IF NOT EXISTS(SELECT 1 FROM Parents_Mas WITH(NOLOCK) WHERE LTRIM(RTRIM(@LoginName))=CASE WHEN @LoginType=1 THEN P_MobileNo ELSE P_EmailId END)
	BEGIN
		SET @ReturnValue=0 -- Parents Not Registered in this Portal
	END
	ELSE IF EXISTS(SELECT 1 FROM Parents_Mas WITH(NOLOCK) WHERE LTRIM(RTRIM(@LoginName))=CASE WHEN @LoginType=1 THEN P_MobileNo ELSE P_EmailId END)
	BEGIN
		IF EXISTS(SELECT 1 FROM UserLogin UL WITH(NOLOCK) 
		INNER JOIN Parents_Mas PM WITH(NOLOCK) ON UL.UL_UserId=PM.P_Id
		WHERE LTRIM(RTRIM(@LoginName))=CASE WHEN @LoginType=1 THEN P_MobileNo ELSE P_EmailId END)
		BEGIN
			--SELECT UL.UL_Id,UL.UL_UserId,UL.UL_LoginName,PM.P_Name,PM.P_Address1,PM.P_Address2,PM.P_CityId,PM.P_EmailId,PM.P_MobileNo 
			--FROM UserLogin UL WITH(NOLOCK) 
			--INNER JOIN Parents_Mas PM WITH(NOLOCK) ON UL.UL_UserId=PM.P_Id
			--WHERE LTRIM(RTRIM(@LoginName))=CASE WHEN @LoginType=1 THEN P_MobileNo ELSE P_EmailId END
			SET @ReturnValue=1	
			SET @ReturnMessage= 'Already registred. Please login'
		END
		ELSE
		BEGIN
			--SELECT 0 as UL_Id,PM.P_Id,'' as UL_LoginName,PM.P_Name,PM.P_Address1,PM.P_Address2,PM.P_CityId,PM.P_EmailId,PM.P_MobileNo 
			--from Parents_Mas PM WITH(NOLOCK) 
			--WHERE LTRIM(RTRIM(@LoginName))=CASE WHEN @LoginType=1 THEN P_MobileNo ELSE P_EmailId END
			SET @ReturnValue=2	
		END
	END

	SELECT @ReturnValue ReturnValue,@ReturnMessage ReturnMessage
END
