namespace GymManagementSystemDAL.Data.Configurations
{
    using GymManagementSystemDAL.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines the <see cref="BookConfiguration"/>
    /// </summary>
    public class BookConfiguration : IEntityTypeConfiguration<Booking>
    {
        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="builder">The
        ///     builder<see cref="EntityTypeBuilder{Booking}"/></param>
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Ignore(X => X.Id);

            builder.HasOne(B => B.member)
                .WithMany(m => m.MembersBooking)
                .HasForeignKey(m => m.MemberId);

            builder.HasOne(sb => sb.session)
                .WithMany(s => s.SessionsBooking)
                .HasForeignKey(s => s.SessionId);

            builder.Property(b => b.CreatedAt)
                .HasColumnName("BookingDate");

            builder.HasKey(B => new { B.SessionId, B.MemberId });
        }
    }
}
