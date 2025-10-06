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
    public class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(TB =>
            {
                TB.HasCheckConstraint("CapacityConstraint", "Capacity BETWEEN 1 AND 25");
                TB.HasCheckConstraint("DateConstraint", "EndDate > StartDate");
            });

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CategoryId);

            builder.HasOne(s => s.Trainer)
                .WithMany(t => t.TrainerSeesions)
                .HasForeignKey(s => s.TrainerId);
        }
    }
}
