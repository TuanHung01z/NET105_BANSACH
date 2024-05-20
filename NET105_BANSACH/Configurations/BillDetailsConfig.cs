using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NET105_BANSACH.Models;

namespace NET105_BANSACH.Configurations
{
    public class BillDetailsConfig : IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.HasKey(p => p.BillDetailsID);
            builder.HasOne(p => p.Bill).WithMany(p => p.BillDetails).
                HasForeignKey(p => p.BillID);
        }
    }
}
