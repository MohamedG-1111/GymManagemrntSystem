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
    public class PlanConfigurations : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Name)
          .HasColumnType("varchar(50)")
          .HasMaxLength(50)
          .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(10,2)")
                .HasPrecision(10, 2);

            builder.Property(p => p.DurationDays)
                .IsRequired();

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Plan_DurationDays", "DurationDays BETWEEN 1 AND 365");
                t.HasCheckConstraint("CK_Plan_Price", "Price >= 0");
            });
        }
    }
}
