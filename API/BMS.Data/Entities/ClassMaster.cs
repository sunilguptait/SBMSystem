using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Class_Mas")]
    public class ClassMaster
    {
        [Key]
        public int Class_Id { get; set; }
        public string Class_Name { get; set; }
        public string Class_ShortName { get; set; }
    }
}
