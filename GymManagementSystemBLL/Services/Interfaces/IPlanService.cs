using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemBLL.ViewModels;
using GymManagementSystemBLL.ViewModels.Plan;
namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> getPlans();
        PlanViewModel ? getPlanDetails(int id);

        PlanToUpdate? GetPlanToUpdate(int id);

        bool UpdatePlan(int id, PlanViewModel plan);    

        bool ToggleStatus(int id);
    }
}
