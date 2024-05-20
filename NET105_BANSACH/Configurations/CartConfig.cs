using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NET105_BANSACH.Models;

namespace NET105_BANSACH.Configurations
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(p => p.Username);
            // Cấu hình quan hệ 1-1
            builder.HasOne(p => p.Account).WithOne(p => p.Cart).
                HasForeignKey<Cart>(p => p.Username);
        }
    }
}
