using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Data;
using GymManagementSystemDAL.Model;
using GymManagementSystemDAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystemDAL.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;
        public SessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Session session)
        {
            _context.sessions.Add(session);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var session = ._context.session.Find(id);
            if (session is null) return 0;
            _dbContext.Sessions.Remove(session);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Session> GetAll() => _dbContext.Sessions.ToList();

        public Session? GetById(int id) => _dbContext.Sessions.Find(id);

        public int Update(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
