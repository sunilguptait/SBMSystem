using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("Book_Mas")]
    public class BookMaster
    {
        [Key]
        public int Book_Id { get; set; }
        public string Book_Name { get; set; }
        public string Book_ShortName { get; set; }
        public int? Book_PublisherId { get; set; }
        public decimal Book_Price { get; set; }
        public DateTime Book_CreationDate { get; set; }
        public int Book_CreatedBy { get; set; }
        public DateTime? Book_EditedDate { get; set; }
        public int? Book_EditedBy { get; set; }
        public int? Book_BSMId { get; set; }
        public int? Book_TypeId { get; set; }
        public string Book_Image { get; set; }

    }
}
