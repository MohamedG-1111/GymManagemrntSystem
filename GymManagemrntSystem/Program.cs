using AutoMapper;
using GymManagementSystemBLL;
using GymManagementSystemBLL.Services.Implementation;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemDAL.Data;
using GymManagementSystemDAL.Data.Repository.Implementation;
using GymManagementSystemDAL.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace GymManagemrntSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IAnalyticalService, AnalyticalService>();


            // builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // builder.Services.AddScoped<IPlanRepository, PlanRepository>();


            var app = builder.Build();

                using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
           GymDataSeeding.SeedData(context);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
