using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NET105_BANSACH.Models;

namespace NET105_BANSACH.Configurations
{
    public class BillConfig : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(p => p.BillID);
            // Khóa ngoại
            builder.HasOne(p => p.Account).WithMany(p => p.Bills).
                HasForeignKey(p => p.Username);
            // builder.HasAlternateKey(); // Set thuộc tính là Unique
        }
    }
}
