using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("BookType_Mas")]
    public class BookTypeMaster
    {
        [Key]
        public int BT_Id { get; set; }
        public string BT_Name { get; set; }
        public string BT_ShortName { get; set; }

    }
}
