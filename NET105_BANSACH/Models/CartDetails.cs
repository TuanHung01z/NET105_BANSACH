using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NET105_BANSACH.Models
{
    public class CartDetails
    {
        [Key]
        public Guid CartDetailsID { get; set; }
        public string? ProductID { get; set; }
        [ForeignKey("Username")]
        public string Username { get; set; } = null!;
        public int Quantity { get; set; }
        public int Status { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual Book? Product { get; set; }
    }
}
