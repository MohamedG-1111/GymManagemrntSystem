using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystemDAL.Data
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=GymManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> categories { get; set; } 

        public DbSet<HealthRecord> healthRecords { get; set; }  

        public DbSet<Member>  Members { get; set; }

        public DbSet<Membership> Memberships { get; set; }  
        public DbSet<Plan> Plans { get; set; }   

        public DbSet<Session> sessions { get; set; }    
        public DbSet<Trainer> trainers { get; set; }    
    }
}
