namespace GymManagementSystemDAL.Data.Configurations
{
    using GymManagementSystemDAL.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="MemberShipConfiguration"/>
    /// </summary>
    public class MemberShipConfiguration : IEntityTypeConfiguration<Membership>
    {
        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="builder">The
        ///     builder<see cref="EntityTypeBuilder{Membership}"/></param>
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

            builder.Ignore(T => T.Id);
        }
    }
}
