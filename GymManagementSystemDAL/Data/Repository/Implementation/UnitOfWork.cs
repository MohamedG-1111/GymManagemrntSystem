using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemDAL.Data.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private readonly ISessionRepository _sessionRepository;
       public Dictionary<Type, Object> Repos = new Dictionary<Type, Object>();
        public UnitOfWork(AppDbContext Context,ISessionRepository sessionRepository) 
        {
            context = Context;
            _sessionRepository= sessionRepository;
        }

        public ISessionRepository SessionRepository { get; }

      
        public IGenericRepository<T> GetGenericRepository<T>() where T : BaseEntity, new()
        {
            var EntityType = typeof(T);
            if (Repos.TryGetValue(EntityType, out var value))
                return (GenericRepository<T>)value;
            var newRepo = new GenericRepository<T>(context);
            Repos.Add(EntityType, newRepo);
            return newRepo;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
