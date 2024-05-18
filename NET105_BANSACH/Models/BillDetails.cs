using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NET105_BANSACH.Models
{
    public class BillDetails
    {
        [Key]
        public Guid BillDetailsID { get; set; }
        [ForeignKey("Bill")]
        public Guid BillID { get; set; }
        public string? ProductID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public virtual Bill? Bill { get; set; }
        public virtual Book? Book { get; set; }
    }
}
