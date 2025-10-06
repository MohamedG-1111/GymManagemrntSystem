using System.Collections.Generic;
using System.Linq;
using GymManagementSystemDAL.Data;
using GymManagementSystemDAL.Model;
using GymManagementSystemDAL.Repository.Interface;

namespace GymManagementSystemDAL.Repository.Implementation
{
    public class PlanRepository : IPlanRepository
    {
        private readonly AppDbContext _context;

        public PlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Plan plan)
        {
            _context.Plans.Add(plan);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan == null) return 0;

            _context.Plans.Remove(plan);
            return _context.SaveChanges();
        }

        public IEnumerable<Plan> GetAll()
        {
            return _context.Plans.ToList();
        }

        public Plan? GetById(int id)
        {
            return _context.Plans.Find(id);
        }

        public int Update(Plan plan)
        {
            _context.Plans.Update(plan);
            return _context.SaveChanges();
        }
    }
}
