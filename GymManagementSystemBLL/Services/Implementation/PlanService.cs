

using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.Plan;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemBLL.Services.Implementation
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public PlanViewModel? getPlanDetails(int id)
        {
            var Plan=unitOfWork.GetGenericRepository<Plan>().Get(id);
            if(Plan == null) return null;
            return new PlanViewModel()
            {
                Name = Plan.Name,
                Description = Plan.Description,
                Id = Plan.Id,
                IsActive = Plan.IsActive,
                Price = Plan.Price,
                Duration = Plan.DurationDays,
            };
        }

        public IEnumerable<PlanViewModel> getPlans()
        {
            var planList = unitOfWork.GetGenericRepository<Plan>().GetAll();
           var PlansView =planList.Select(x => new PlanViewModel()
            {
               Name = x.Name,
               Description = x.Description, 
               Id = x.Id,
               IsActive = x.IsActive,
               Price = x.Price,
               Duration=x.DurationDays,
            });
            return PlansView;   
        }

        public PlanToUpdate? GetPlanToUpdate(int id)
        {
            var plan = unitOfWork.GetGenericRepository<Plan>().Get(id);
            
            if (plan == null || plan.IsActive==false || HasActiveMembership(id)) return null;
    
            return new PlanToUpdate()
            {
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDay = plan.DurationDays,
            };
        }

        public bool ToggleStatus(int id)
        {
            var Repo = unitOfWork.GetGenericRepository<Plan>();
            var plan= Repo.Get(id);
            if(plan is null || HasActiveMembership(id)) return false;
            if (plan.IsActive)
                plan.IsActive = false;
            else
                plan.IsActive = true;
            plan.UpdateAt = DateTime.Now;
            try
            {
                Repo.Update(plan);
                return unitOfWork.SaveChanges()>0;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePlan(int id, PlanViewModel plan)
        {
            var Repo = unitOfWork.GetGenericRepository<Plan>();
            var p = Repo.Get(id);
            if(p == null || HasActiveMembership(id)) return false;
            try
            {
                (p.Description, p.Price, p.DurationDays,p.UpdateAt) =
               (plan.Description, plan.Price, plan.Duration, DateTime.Now);
                Repo.Update(p);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        #region Helper
        private bool HasActiveMembership(int id)
        {
            var HasMemberShip = unitOfWork.GetGenericRepository<Membership>()
                .GetAll(x => x.PlanId == id && x.Status == "Active").Any();
            return HasMemberShip;
        }
        #endregion
    }
}
