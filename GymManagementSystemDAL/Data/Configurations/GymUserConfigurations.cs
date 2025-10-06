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
    public class GymUserConfigurations<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(t => t.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(t => t.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.HasIndex(t => t.Email).IsUnique();
            

            builder.Property(t=>t.Phone)
                .HasMaxLength (11);
            builder.HasIndex(t => t.Phone).IsUnique();

            builder.Property(t => t.gender)
                .HasConversion<string>();


            builder.ToTable(t =>
            {
                t.HasCheckConstraint("EmailConstraint", "Email Like '_%@_%._%'");
                t.HasCheckConstraint("PhoneConstraint", "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");
            });

            builder.OwnsOne(T => T.Address, tb =>
            {
                tb.Property(a => a.BuildingNumber)
                .HasColumnName("BuildingNumber");

                tb.Property(a => a.City)
                .HasColumnType("varchar")
                .HasColumnName("City")
                .HasMaxLength(30);

                tb.Property(a => a.Streat)
               .HasColumnType("varchar")
               .HasColumnName("Streat")
               .HasMaxLength(30);
            });
                
                
        }
    }
}
