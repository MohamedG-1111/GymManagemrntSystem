using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemDAL.Repository.Interface
{
    public interface ItrainerRepository
    {
        int Add(Trainer trainer);
        int Update(Trainer trainer);
         int Delete(int Id);
        IEnumerable<Trainer> GetAll();
        Trainer? GetTrainer(int id);
    }
}
