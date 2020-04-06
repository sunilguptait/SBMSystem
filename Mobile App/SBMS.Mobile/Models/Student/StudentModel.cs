using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XF.Material.Forms.Models;

namespace SBMS.Mobile.Models.Student
{
    public class StudentModel
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
        public bool? IsBooksPurchased { get { return !string.IsNullOrEmpty(OrderCode); } }
    }
    public class StudentSearchModel : BaseParametersModel
    {
        public int SchoolId { get; set; } = 0;
        public int ClassId { get; set; } = 0;
        public int ParentsId { get { return UserId; } }
        public string EnrollmentNo { get; set; }
    }
}
