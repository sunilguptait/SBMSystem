using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data;
using BMS.Data.Entities;
using BMS.Services.Common;
using BMS.Services.Users;
using BMS.ViewModels.Common;
using BMS.ViewModels.Student;
using BMS.ViewModels.User;

namespace BMS.Services.Student
{
    public class StudentService : IStudentService
    {
        BMSContext bMSContext = new BMSContext();
        ICommonService _commonService;
        IUserService _userService;
        public StudentService(ICommonService commonService, IUserService userService)
        {
            _commonService = commonService;
            _userService = userService;
        }
        public StudentMaster GetStudentById(int id)
        {
            return bMSContext.StudentMaster.Where(m => m.St_id == id).FirstOrDefault();
        }
        public StudentMaster SaveStudent(StudentMasterVM model)
        {
            var entity = GetStudentById(Convert.ToInt32(model.Id));
            if (entity == null)
            {
                entity = new StudentMaster();
            }
            entity.St_SchoolId = model.SchoolId;
            entity.St_ParentId = model.ParentId;
            entity.St_Name = model.Name;
            entity.St_DateOfBirth = model.DOB;
            entity.St_EnrollmentNo = model.EnrollmentNo;
            if (entity.St_id == 0)
            {
                bMSContext.StudentMaster.Add(entity);
            }
            bMSContext.SaveChanges();
            model.Id = entity.St_id;
            SaveStudentEnrollment(model);
            return entity;
        }
        public StudentEnrollment GetCurrentEnrollmentDetails(StudentMasterVM model, SessionMaster currentSession)
        {
            var enrollmentDetails = bMSContext.StudentEnrollment.Where(a => a.SE_SessionId == currentSession.Session_Id && a.SE_StudentId == model.Id).FirstOrDefault();
            return enrollmentDetails;
        }
        public StudentEnrollment SaveStudentEnrollment(StudentMasterVM model)
        {
            var currentSession = _commonService.GetCurrentSession();
            var entity = GetCurrentEnrollmentDetails(model, currentSession);
            if (entity == null)
            {
                entity = new StudentEnrollment();
                entity.SE_CreationDate = System.DateTime.Now;
                entity.SE_CreatedBy = model.UserId;
            }
            entity.SE_StudentId = model.Id;
            entity.SE_ClassId = model.ClassId;
            entity.SE_ParentId = model.ParentId;
            entity.SE_SessionId = currentSession.Session_Id;
            if (entity.SE_Id == 0)
            {
                bMSContext.StudentEnrollment.Add(entity);
            }
            else
            {
                entity.SE_EditedBy = model.UserId;
                entity.SE_EditedDate = System.DateTime.Now;
            }
            bMSContext.SaveChanges();
            return entity;
        }
        public List<StudentMasterVM> GetStudents(StudentMasterSearchVM searchModel)
        {
            var studentId = new SqlParameter { ParameterName = "@StudentId", Value = searchModel.StudentId };
            var schoolId = new SqlParameter { ParameterName = "@SchoolId", Value = searchModel.SchoolId };
            var classId = new SqlParameter { ParameterName = "@ClassId", Value = searchModel.ClassId };
            var parentsId = new SqlParameter { ParameterName = "@ParentsId", Value = searchModel.ParentsId };
            var enrollmentNo = new SqlParameter { ParameterName = "@EnrollmentNo", Value = (object)searchModel.EnrollmentNo ?? DBNull.Value };
            var pageIndex = new SqlParameter { ParameterName = "@PageIndex", Value = searchModel.PageIndex };
            var pageSize = new SqlParameter { ParameterName = "@PageSize", Value = searchModel.PageSize };
            var studentName = new SqlParameter { ParameterName = "@StudentName", Value = searchModel.StudentName ?? "" };
            var bookSellerId = new SqlParameter { ParameterName = "@BookSellerId", Value = searchModel.BookSellerId };

            var students = bMSContext.Database.SqlQuery<StudentMasterVM>("EXEC GetStudentsList @StudentId,@SchoolId,@ClassId,@ParentsId,@EnrollmentNo,@PageIndex,@PageSize,@StudentName,@BookSellerId",
                    studentId, schoolId, classId, parentsId, enrollmentNo, pageIndex, pageSize, studentName, bookSellerId).ToList();

            return students;
        }
        public PagedList<StudentMasterVM> GetStudentsByLinq(StudentMasterSearchVM searchModel)
        {
            var currentSession = _commonService.GetCurrentSession();
            var students = (from SM in bMSContext.StudentMaster
                            join School in bMSContext.SchoolMaster on SM.St_SchoolId equals School.SM_Id
                            join PM in bMSContext.ParentsMaster on SM.St_ParentId equals PM.P_Id
                            join SE in bMSContext.StudentEnrollment.Where(a => a.SE_SessionId == currentSession.Session_Id) on SM.St_id equals SE.SE_StudentId into Enrollment
                            from SE in Enrollment.DefaultIfEmpty()
                            join CM in bMSContext.ClassMaster on SE.SE_ClassId equals CM.Class_Id
                            where (string.IsNullOrEmpty(searchModel.EnrollmentNo) || SM.St_EnrollmentNo == searchModel.EnrollmentNo)
                            && (searchModel.SchoolId == 0 || SM.St_SchoolId == searchModel.SchoolId)
                            && (searchModel.ClassId == 0 || CM.Class_Id == searchModel.ClassId)
                            && (searchModel.ParentsId == 0 || SM.St_ParentId == searchModel.ParentsId)
                            && (searchModel.StudentId == 0 || SM.St_id == searchModel.StudentId)
                            orderby SM.St_Name
                            select new StudentMasterVM()
                            {
                                Id = SM.St_id,
                                Name = SM.St_Name,
                                DOB = SM.St_DateOfBirth,
                                EnrollmentNo = SM.St_EnrollmentNo,
                                ParentId = SM.St_ParentId,
                                ParentName = PM.P_Name,
                                SchoolId = SM.St_SchoolId,
                                SchoolName = School.SM_Name,
                                ClassId = CM.Class_Id,
                                ClassName = CM.Class_Name,
                                EnrollmentId = SE.SE_Id,
                            });

            return new PagedList<StudentMasterVM>(students, searchModel.PageIndex, searchModel.PageSize, new string[] { searchModel.SortDirection }, new string[] { searchModel.SortBy });
        }

        //
        public Tuple<StudentMaster,string> BookSellerSaveStudent(StudentMasterVM model)
        {
            using (DbContextTransaction transaction = bMSContext.Database.BeginTransaction())
            {
                try
                {
                    var isUserExists = _userService.ValidateUserOnParentsRegistration(new ViewModels.User.ValidateParentsUserNameVM() { LoginName = model.MobileNo, LoginType = 1 });
                    if(isUserExists.ReturnValue<0) // return if mobile number is already registred with book seller or employee.
                    {
                        return new Tuple<StudentMaster, string>(null, isUserExists.ReturnMessage);
                    }

                    if (isUserExists.ReturnValue == 0) // REGISTER PARENTS IF NOT REGISTRED.
                    {
                        var parentsModel = new ParentsRegistrationVM();
                        parentsModel.MobileNo = model.MobileNo;
                        parentsModel.Name = model.ParentName;
                        var parentsDetails = _userService.ParentsRegistration(parentsModel);
                        if (string.IsNullOrEmpty(parentsDetails.Item2))
                            model.ParentId = parentsDetails.Item1.P_Id;
                    }
                    else // GET PARENTS ID IF ALREADY REGISTRED
                    {
                        model.ParentId = isUserExists.UserId;
                    }
                    if (model.ParentId > 0)
                    {
                        var entity=SaveStudent(model);
                        transaction.Commit();
                        return new Tuple<StudentMaster, string>(entity, string.Empty);
                    }
                    else
                    {
                        transaction.Rollback();
                        return new Tuple<StudentMaster, string>(null, "Error occurred while registration. Please try again and contact to system admin.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Tuple<StudentMaster, string>(null, "Error occurred while registration. Please try again and contact to system admin.");
                }
            }
        }

        public Tuple<ImportStudentMainVM, bool> ImportStudents(ImportStudentMainVM model)
        {
            var dataXML = CommonMethods.SerializeXML(model.Students);

            var XMLData = new SqlParameter { ParameterName = "@XMLData", Value = dataXML };
            var schoolId = new SqlParameter { ParameterName = "@SchoolId", Value = model.SchoolId };
            var isValid = new SqlParameter { ParameterName = "@IsValid", Value = true, Direction = ParameterDirection.Output };
          

            var response= bMSContext.Database.SqlQuery<ImportStudentVM>("EXEC ValidateAndImportStudents @XMLData,@SchoolId,@IsValid OUTPUT",
                    XMLData,schoolId, isValid).ToList();
            model.Students = response;
            return new Tuple<ImportStudentMainVM, bool>(model, Convert.ToBoolean(isValid.Value));
        }

    }
}
