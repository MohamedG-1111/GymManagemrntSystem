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
    public class GenericRepository<IEntity> : IGenericRepository<IEntity> where IEntity : BaseEntity, new()
    {
        private readonly AppDbContext _Context;

        public GenericRepository(AppDbContext context)
        {
            _Context = context;
        }
        public int Add(IEntity entity)
        {
          _Context.Set<IEntity>().Add(entity);
            return _Context.SaveChanges();  
        }

        public int Delete(IEntity entity)
        {
            _Context.Set<IEntity>().Remove(entity);
            return _Context.SaveChanges();
        }

        public IEntity? Get(int id) => _Context.Set<IEntity>().Find(id);

     

        public IEnumerable<IEntity> GetAll()=> _Context.Set<IEntity>().AsNoTracking().ToList();


        public int Update(IEntity entity)
        {
            _Context.Set<IEntity>().Update(entity);
            return _Context.SaveChanges();
        }
    }
}
