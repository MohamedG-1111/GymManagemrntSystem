using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystemDAL.Data.Configurations
{
    public class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members").HasKey(T=>T.Id);

            builder.HasOne<Member>()
                .WithOne(e => e.HealthRecord)
                .HasForeignKey<HealthRecord>(e=>e.Id);
            builder.Ignore(e => e.CreatedAt);
            builder.Ignore(e => e.UpdateAt);
        }
    }
}
