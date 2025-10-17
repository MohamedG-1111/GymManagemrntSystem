using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemDAL.Data.Repository.Interface
{
    public interface IUnitOfWork
    {
        public ISessionRepository SessionRepository { get; }
        public IGenericRepository<T> GetGenericRepository<T>() where T : BaseEntity,new();

        int SaveChanges();
    }
}
