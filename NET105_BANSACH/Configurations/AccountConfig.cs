using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET105_BANSACH.Models;

namespace NET105_BANSACH.Configurations
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Khóa chính
            builder.HasKey(p => p.Username);
            // Key có nhiều cột
            // builder.HasKey(p => new {p.Username, p.Password}); // key có 2 cột
            // builder.HasNoKey(); // Không có khóa
            // Cấu hình thuộc tính
            builder.Property(p => p.Password).HasColumnType("varchar(256)");
            builder.Property(p => p.Address).IsUnicode(true).IsFixedLength(true).HasMaxLength(256);
            // .IsUnicode(true).IsFixedLength(true).HasMaxLength(256) tương đương với nvarchar(256)

        }
    }
}
