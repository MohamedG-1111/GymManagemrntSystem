namespace GymManagementSystemDAL.Data.Configurations
{
    using GymManagementSystemDAL.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="MemberConfigurations"/>
    /// </summary>
    public class MemberConfigurations : GymUserConfigurations<Member>, IEntityTypeConfiguration<Member>
    {
        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="builder">The
        ///     builder<see cref="EntityTypeBuilder{Member}"/></param>
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
