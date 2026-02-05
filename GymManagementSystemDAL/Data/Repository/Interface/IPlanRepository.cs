using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemDAL.Data.Repository.Interface
{
    public interface IPlanRepository
    {
        IEnumerable<Plan> GetAll();

        Plan ? GetById(int id);

        int Update(Plan plan);


    }
}
