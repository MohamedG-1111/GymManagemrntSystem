using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystemDAL.Data.Repository.Implementation
{
    public class SessionRepository : GenericRepository<Session>,ISessionRepository
    {
        private readonly AppDbContext context;

        public SessionRepository(AppDbContext context):base(context) 
        {
            this.context = context;
        }

        public IEnumerable<Session> GetAllSessionsWithCategoryAndTrainers()
        {
           return context.Sessions.Include(S=>S.Category).Include(S=>S.Trainer).ToList();
        }


        public int GetCountOfBookedSloted(int Id)
        {
            return context.Bookings.Count(x => x.SessionId == Id);
        }

        public Session? GetSessionsWithCategoryAndTrainers(int Id)
        {
            return context.Sessions.Include(S => S.Category).Include(S => S.Trainer).FirstOrDefault(x=>x.Id==Id);

        }
    }
}
