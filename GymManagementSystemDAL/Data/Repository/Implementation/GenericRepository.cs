using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystemDAL.Data.Repository.Implementation
{
    public class GenericRepository<IEntity> : IGenericRepository<IEntity> where IEntity : BaseEntity, new()
    {
        private readonly AppDbContext _Context;

        public GenericRepository(AppDbContext context)
        {
            _Context = context;
        }
        public void Add(IEntity entity)
        {
          _Context.Set<IEntity>().Add(entity);
            
        }

        public void Delete(IEntity entity)
        {
            _Context.Set<IEntity>().Remove(entity);
           
        }

        public IEntity? Get(int id) => _Context.Set<IEntity>().Find(id);

        public IEnumerable<IEntity> GetAll(Expression<Func<IEntity, bool>>? condition = null)
        {
            if(condition == null)
                return _Context.Set<IEntity>().AsNoTracking().ToList();
            else
                return _Context.Set<IEntity>().Where(condition).ToList();    

        }

        public void Update(IEntity entity)
        {
            _Context.Set<IEntity>().Update(entity);
           
        }
    }
}
