namespace GymManagemrntSystem
{
    using GymManagementSystemBLL;
    using GymManagementSystemBLL.Services.Implementation;
    using GymManagementSystemBLL.Services.Interfaces;
    using GymManagementSystemDAL.Data;
    using GymManagementSystemDAL.Data.Repository.Implementation;
    using GymManagementSystemDAL.Data.Repository.Interface;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="Program"/>
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/></param>
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
            builder.Services.AddScoped<IMemberServices, MemberServices>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();

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
