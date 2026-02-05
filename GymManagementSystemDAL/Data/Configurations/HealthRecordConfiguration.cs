namespace GymManagementSystemDAL.Data.Configurations
{
    using GymManagementSystemDAL.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="HealthRecordConfiguration"/>
    /// </summary>
    public class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="builder">The
        ///     builder<see cref="EntityTypeBuilder{HealthRecord}"/></param>
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members").HasKey(T => T.Id);

            builder.HasOne<Member>()
                .WithOne(e => e.HealthRecord)
                .HasForeignKey<HealthRecord>(e => e.Id);
            builder.Ignore(e => e.CreatedAt);
            builder.Ignore(e => e.UpdateAt);
        }
    }
}
