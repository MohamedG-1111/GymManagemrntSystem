using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;
using Microsoft.Identity.Client;

namespace GymManagementSystemDAL.Data.Repository.Interface
{
    public interface ISessionRepository:IGenericRepository<Session>
    {
        public IEnumerable<Session> GetAllSessionsWithCategoryAndTrainers();

        public int GetCountOfBookedSloted(int Id);
        public Session? GetSessionsWithCategoryAndTrainers(int Id);

    }
}
