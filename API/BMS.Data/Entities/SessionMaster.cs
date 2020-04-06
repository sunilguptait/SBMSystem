using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Session_Mas")]
    public class SessionMaster
    {
        [Key]
        public int Session_Id { get; set; }
        public string Session_Name { get; set; }
        public string Session_ShortName { get; set; }
        public bool? Session_IsCurrent { get; set; }

    }
}
