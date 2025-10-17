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
    public class BookConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Ignore(X=>X.Id);

            builder.HasOne(B => B.member)
                .WithMany(m => m.MembersBooking)
                .HasForeignKey(m => m.MemberId);

            builder.HasOne(sb => sb.session)
                .WithMany(s => s.SessionsBooking)
                .HasForeignKey(s => s.SessionId);

            builder.Property(b => b.CreatedAt)
                .HasColumnName("BookingDate");

            builder.HasKey(B => new { B.SessionId, B.MemberId});
                
        }
    }
}
