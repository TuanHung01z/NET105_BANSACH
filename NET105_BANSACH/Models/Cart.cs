using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NET105_BANSACH.Models
{
    public class Cart
    {
        [Key]
        [ForeignKey("Account")]
        public string Username { get; set; } = null!;
        public int Status { get; set; }
        public virtual List<CartDetails> Details { get; set; }
        public Account? Account { get; set; }
    }
}
