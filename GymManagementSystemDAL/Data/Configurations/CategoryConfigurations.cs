namespace GymManagementSystemDAL.Data.Configurations
{
    using GymManagementSystemDAL.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="CategoryConfigurations"/>
    /// </summary>
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="builder">The
        ///     builder<see cref="EntityTypeBuilder{Category}"/></param>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(20);
        }
    }
}
