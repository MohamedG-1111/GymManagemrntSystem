using System.Collections.Generic;
using System.Linq;
using GymManagementSystemDAL.Data;
using GymManagementSystemDAL.Model;
using GymManagementSystemDAL.Repository.Interface;

namespace GymManagementSystemDAL.Repository.Implementation
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
            var session = _context.sessions.Find(id);
            if (session == null) return 0;

            _context.sessions.Remove(session);
            return _context.SaveChanges();
        }

        public IEnumerable<Session> GetAll()
        {
            return _context.sessions.ToList();
        }

        public Session? GetById(int id)
        {
            return _context.sessions.Find(id);
        }

        public int Update(Session session)
        {
            _context.sessions.Update(session);
            return _context.SaveChanges();
        }
    }
}
