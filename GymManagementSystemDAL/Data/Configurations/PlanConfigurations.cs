namespace GymManagementSystemDAL.Data.Configurations
{
    using GymManagementSystemDAL.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="PlanConfigurations"/>
    /// </summary>
    public class PlanConfigurations : IEntityTypeConfiguration<Plan>
    {
        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="builder">The
        ///     builder<see cref="EntityTypeBuilder{Plan}"/></param>
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
