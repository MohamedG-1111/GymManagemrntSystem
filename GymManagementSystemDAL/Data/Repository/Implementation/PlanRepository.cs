using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemDAL.Data.Repository.Implementation
{
    public class PlanRepository : IPlanRepository
    {
        private readonly AppDbContext _Context;

        public PlanRepository(AppDbContext context) 
        {
               _Context=context;
        }
        public IEnumerable<Plan> GetAll()=>_Context.Plans.ToList();



        public Plan? GetById(int id) => _Context.Plans.Find(id);
       
        public int Update(Plan plan)
        {
            _Context.Plans.Add(plan);
            return _Context.SaveChanges();
        }
    }
}
