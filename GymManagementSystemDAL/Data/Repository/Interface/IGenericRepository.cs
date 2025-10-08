using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemDAL.Data.Repository.Interface
{
    public interface IGenericRepository<IEntity> where IEntity : BaseEntity, new()  
    {
        IEnumerable<IEntity> GetAll();
        IEntity? Get(int id);

        int Add(IEntity entity);

        int Update(IEntity entity);
        int Delete(IEntity entity);

    }
}
