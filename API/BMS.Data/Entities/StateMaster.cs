using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("State_Mas")]
    public class StateMaster
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }

    }
}
