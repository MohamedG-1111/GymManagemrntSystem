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
    public class MemberShipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(mp => mp.CreatedAt)
                 .HasColumnName("StartDate");
            builder.HasKey(x => new { x.PlanId, x.MemberId });
            builder.HasOne(e => e.plan)
                .WithMany(p => p.PlanMembers)
                .HasForeignKey(x => x.PlanId);

            builder.HasOne(ml => ml.member)
                .WithMany(m => m.Membersplans)
                .HasForeignKey(ml => ml.MemberId);

            builder.Ignore(T=>T.Id);
        }
    }
}
