using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Data;
using GymManagementSystemDAL.Model;
using GymManagementSystemDAL.Repository.Interface;

namespace GymManagementSystemDAL.Repository.Implementation
{
    public class TrainerRepository : ItrainerRepository
    {
        private readonly AppDbContext _context;

        public TrainerRepository(AppDbContext context)
        {
            _context=context;
        }
        public int Add(Trainer trainer)
        {
            _context.trainers.Add(trainer);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var trainer = _context.trainers.Find(id);
            if (trainer == null) return 0;

            _context.trainers.Remove(trainer);
            return _context.SaveChanges();
        }

        public IEnumerable<Trainer> GetAll()
        {
            return _context.trainers.ToList();
        }

        public Trainer? GetTrainer(int id)
        {
            return _context.trainers.Find(id);
        }

        public int Update(Trainer trainer)
        {
            _context.trainers.Update(trainer);
            return _context.SaveChanges();
        }
    }
}
