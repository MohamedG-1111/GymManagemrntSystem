using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemDAL.Data
{
    public static class GymDataSeeding
    {
       public static bool SeedData(AppDbContext dbContext)
        {
            try
            {
                var PlanHasData = dbContext.Plans.Any();
                var CategoryHasData = dbContext.Categories.Any();
                if (!PlanHasData)
                {
                    var plans = LoadFiles<Plan>("plans.json");
                    dbContext.Plans.AddRange(plans);
                }
                if (!CategoryHasData)
                {
                    var categories = LoadFiles<Category>("categories.json");
                    dbContext.Categories.AddRange(categories);
                }
                return dbContext.SaveChanges() > 0;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static List<T> LoadFiles<T>(string FileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FileName);
            if (!File.Exists(path))
                throw new FileNotFoundException();

            var Data = File.ReadAllText(path);
            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            Options.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<List<T>>(Data, Options) ?? [];
        }

    }
}
