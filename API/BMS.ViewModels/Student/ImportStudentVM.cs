using BMS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels.Student
{
    public class ImportStudentVM
    {
        public int SerialNo { get; set; }
        public string StudentName { get; set; }
        public string ParentsName { get; set; }
        public string Class { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string MobileNo { get; set; }
        public string EnrollmentNo { get; set; }
        public string DOB { get; set; }
        public string ErrorMessage { get; set; }

    }
    public class ImportStudentMainVM
    {
        public ImportStudentMainVM()
        {
            Students = new List<ImportStudentVM>();
        }
        public int SchoolId { get; set; } = 0;
        public List<ImportStudentVM> Students { get; set; }

    }
}
