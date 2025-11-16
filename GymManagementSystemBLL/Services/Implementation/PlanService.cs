

using AutoMapper;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.Plan;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

namespace GymManagementSystemBLL.Services.Implementation
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlanService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public PlanViewModel? getPlanDetails(int id)
        {
            var Plan=unitOfWork.GetGenericRepository<Plan>().Get(id);
            if(Plan == null) return null;
           return mapper.Map<PlanViewModel>(Plan);  
        }

        public IEnumerable<PlanViewModel> getPlans()
        {
            var planList = unitOfWork.GetGenericRepository<Plan>().GetAll();
            return mapper.Map<IEnumerable<PlanViewModel>>(planList);    
        }

        public PlanToUpdate? GetPlanToUpdate(int id)
        {
            var plan = unitOfWork.GetGenericRepository<Plan>().Get(id);
            
            if (plan == null || plan.IsActive==false || HasActiveMembership(id)) return null;
            return mapper.Map<PlanToUpdate>(plan);
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
                mapper.Map(plan, p);
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
        .GetAll(x => x.PlanId == id)   
        .AsEnumerable()                
        .Any(x => x.Status == "Active"); 
            return HasMemberShip;
        }
        #endregion
    }
}
