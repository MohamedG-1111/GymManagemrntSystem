using GymManagementSystemBLL.Services.Implementation;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.Member;
using GymManagementSystemBLL.ViewModels.Trainer;
using Microsoft.AspNetCore.Mvc;

namespace GymManagemrntSystem.Controllers
{
    public class TrainerController:Controller
    {
        private readonly ITrainerService TrainerService;


        public TrainerController(ITrainerService trainerServices)
        {
            this.TrainerService = trainerServices;
        }

        public IActionResult Index()
        {
            var Data = TrainerService.GetTrainers();
            return View(Data);
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreateTrainer(CreateTrainerModel createTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("IvaildData", "Check Data and Missing fields");
                return View(nameof(Create), createTrainer);

            }
            if (TrainerService.CreateTrainer(createTrainer))
            {
                TempData["SuccessMessage"] = "Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Not Created";
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Not 0 or Negative";
                return RedirectToAction(nameof(Index));
            }

            var trainer = TrainerService.GetDetails(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Not Found";

                return RedirectToAction(nameof(Index));

            }
            return View(trainer);
        }


        public IActionResult EditTrainer(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Not 0 or Negative";
                return RedirectToAction(nameof(Index));
            }


            var TrainerToUpdate = TrainerService.GetTrainerToUpdate(id);
            if (TrainerToUpdate == null)
            {
                TempData["ErrorMessage"] = "Not Found";
                return RedirectToAction(nameof(Index));

            }

            return View(TrainerToUpdate);
        }
        [HttpPost]
        public IActionResult EditTrainer([FromRoute] int id, TrainerToUpdateView TrainerToUpdate)
        {
            if (!ModelState.IsValid)

                return View(TrainerToUpdate);
            var result = TrainerService.Update(id, TrainerToUpdate);
            if (result)
            {
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Not Updated";
            }
            return RedirectToAction(nameof(Index));

        }


        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Not 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = TrainerService.GetDetails(id);
            if (Trainer == null)
            {
                TempData["ErrorMessage"] = "Not Found";

                return RedirectToAction(nameof(Index));

            }
            ViewBag.id = id;
            return View();
        }
        
        public IActionResult DeletedConfirmed([FromForm]int id)
        {
            var result = TrainerService.Delete(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Not Deleted";
            }
            return RedirectToAction(nameof(Index));
        }





    }
}
