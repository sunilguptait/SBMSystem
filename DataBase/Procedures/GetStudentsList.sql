USE [SBMS]
IF OBJECT_ID ('GetStudentsList') IS NOT NULL
   DROP PROCEDURE GetStudentsList
GO

GO
/****** Object:  StoredProcedure [dbo].[GetStudentsList]    Script Date: 1/12/2020 3:06:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ASHOK KUMAWAT
-- Create date: 1-JAN-2020
-- Description:	TO GET STUDENTS LIST
-- =============================================
-- GetStudentsList @ParentsId=2
CREATE PROCEDURE [dbo].[GetStudentsList]
	@StudentId INT=0,
	@SchoolId INT=0,
	@ClassId  INT=0,
	@ParentsId INT=0,
	@EnrollmentNo VARCHAR(50)='',
	@PageIndex INT =1,
	@PageSize INT=20,
	@StudentName VARCHAR(100)='',
	@BookSellerId INT=0
	
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @SessionId INT=0

	--GET CURRENT SESSION
	SELECT @SessionId=Session_id FROM Session_Mas WHERE Session_IsCurrent=1

	SELECT SM.St_id Id,SM.St_Name Name,SM.St_EnrollmentNo EnrollmentNo,SE.SE_Id EnrollmentId,SM.St_DateOfBirth DOB,
	SM.St_ParentId ParentId,PM.P_Name ParentName,SM.St_SchoolId SchoolId,School.SM_Name SchoolName,CM.Class_Id ClassId,
	CM.Class_Name ClassName,CM.Class_ShortName ClassSortName,SE.SE_Id EnrollmentId,OM.Order_Code OrderCode
	FROM Student_Mas SM
	INNER JOIN Parents_Mas PM ON SM.St_ParentId=PM.P_Id
	LEFT JOIN School_Mas School ON SM.St_SchoolId=School.SM_Id
	LEFT JOIN Student_Enrollment SE ON SM.St_id=SE.SE_StudentId AND SE.SE_SessionId=@SessionId
	LEFT JOIN Class_Mas CM ON CM.Class_Id=SE.SE_ClassId
	LEFT JOIN Order_Mas OM ON OM.Order_SEId=SE.SE_Id
	LEFT JOIN SellerSchool_Mapping SS ON SS.SSM_SMId=School.SM_Id
	WHERE (@StudentId=0 OR SM.St_id=@StudentId)
	AND (@SchoolId=0 OR SM.St_SchoolId=@SchoolId)
	AND (@ClassId=0 OR SE.SE_ClassId=@ClassId)
	AND (@ParentsId=0 OR PM.P_Id=@ParentsId)
	AND (ISNULL(@EnrollmentNo,'')='' OR SM.St_EnrollmentNo=@EnrollmentNo)
	AND (ISNULL(@StudentName,'')='' OR SM.St_Name LIKE '%'+@StudentName+'%')
	AND (ISNULL(@BookSellerId,0)=0 OR SS.SSM_BSMId=@BookSellerId)

	--ORDER BY Name OFFSET @PageIndex * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;

END
