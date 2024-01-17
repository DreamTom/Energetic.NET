using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Infrastructure.EnitiyConfigs
{
    public class UserOperationHistoryEntityConfig : IEntityTypeConfiguration<UserOperationHistory>
    {
        public void Configure(EntityTypeBuilder<UserOperationHistory> builder)
        {
            builder.Property(f => f.RequestMethod)
                    .HasConversion<string>()
                    .HasMaxLength(10);
        }
    }
}
