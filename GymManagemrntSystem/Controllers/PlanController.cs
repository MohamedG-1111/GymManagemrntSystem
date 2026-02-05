using GymManagementSystemBLL.Services.Implementation;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.Plan;
using Microsoft.AspNetCore.Mvc;

namespace GymManagemrntSystem.Controllers
{
    public class PlanController:Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }

        public ActionResult Index()
        {
            var Data = planService.getPlans();
            return View(Data);
        }
        public ActionResult PlanDetails(int id)
        {
            if(id < 0)
            {
                TempData["ErrorMessage"] = "Invaild Plan Id";
                return RedirectToAction(nameof(Index));
            }
            var PlanDetails = planService.getPlanDetails(id);
            if (PlanDetails is null)
            {
                TempData["ErrorMessage"] = "Plan is Not Found";
                return RedirectToAction(nameof(Index));

            }
            return View(PlanDetails);
        }
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invaild Plan Id";
                return RedirectToAction(nameof(Index));
            }
            var PlanToUpdate= planService.GetPlanToUpdate(id);
            if (PlanToUpdate is null)
            {
                TempData["ErrorMessage"] = "Can not Update";
                return RedirectToAction(nameof(Index));

            }
            return View(PlanToUpdate);
        }
        [HttpPost]
        public ActionResult Edit(int id, PlanViewModel planToUpdate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data");
            }
            var Plan = planService.UpdatePlan(id, planToUpdate);
            if(!Plan)
            {
                TempData["SucessMessage"] = "Successfully Updated";
            }
            else
            {
                TempData["ErrorMessage"] = "Can not Update";

            }
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Activate(int id)
        {
            var result = planService.ToggleStatus(id);
            if (!result)
            {
                 TempData["ErrorMessage"] = "Can not Chanage";
            }
            else
            {
                TempData["SucessMessage"] = "Done";
            }
            return RedirectToAction(nameof(Index));
        }



    }
}
