using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NET105_BANSACH.Models;

namespace NET105_BANSACH.Configurations
{
    public class CartDetailsConfig : IEntityTypeConfiguration<CartDetails>
    {
        public void Configure(EntityTypeBuilder<CartDetails> builder)
        {
            builder.HasKey(x => x.CartDetailsID);
            builder.HasOne(p => p.Cart).WithMany(p => p.Details).
                HasForeignKey(p => p.Username).HasConstraintName("FK_Cart_CartDetails");
        }
    }
}
