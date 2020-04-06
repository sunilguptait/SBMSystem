using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entities
{
    [Table("BooksClass_Mapping")]
    public class BooksClassMapping
    {
        [Key]
        public int BCM_Id { get; set; }
        public int BCM_BookId { get; set; }
        public int BCM_ClassId { get; set; }
        public bool? BCM_IsDeleted { get; set; }
        public bool? BCM_OutOfStock { get; set; }
        public DateTime BCM_CreationDate { get; set; }
        public int BCM_CreatedBy { get; set; }
        public int? BCM_EditedBy { get; set; }
        public DateTime? BCM_EditedDate { get; set; }
        public int? BCM_DefaultQty { get; set; }
        

    }
}
