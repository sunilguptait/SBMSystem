using BMS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels.Student
{
    public class StudentMasterVM : BaseViewModel
    {
        public int Id { get; set; }
        public int? EnrollmentId { get; set; }
        public string Name { get; set; }
        public int? SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string EnrollmentNo { get; set; }
        public int? ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassSortName { get; set; }
        public DateTime? DOB { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        public string OrderCode { get; set; }
        public string MobileNo { get; set; }
    }
    public class StudentMasterSearchVM : BaseSearchModel
    {
        public int StudentId { get; set; } = 0;
        public int SchoolId { get; set; } = 0;
        public int ClassId { get; set; } = 0;
        public string EnrollmentNo { get; set; }
        public string StudentName { get; set; }
    }
}
