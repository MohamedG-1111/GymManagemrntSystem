using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemDAL.Data.Repository.Interface
{
    public interface IGenericRepository<IEntity> where IEntity : BaseEntity, new()  
    {
        IEnumerable<IEntity> GetAll(Expression<Func<IEntity,bool>>? condition = null);
        IEntity? Get(int id);

        void Add(IEntity entity);
        
        void Update(IEntity entity);
        void Delete(IEntity entity);

    }
}
