using GymManagementSystemBLL.Services.Implementation;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;

namespace GymManagemrntSystem.Controllers
{
    public class SessionController:Controller
    {
        private readonly ISessionServices sessionServices;

        public SessionController(ISessionServices SessionServices)
        {
            sessionServices = SessionServices;
        }
        public ActionResult Index()
        {
            var Sessions = sessionServices.GetAll();
            return View(Sessions);
        }
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invaild Session Id";
                return RedirectToAction(nameof(Index));
            }
            var SessionDetails = sessionServices.GetById(id);
            if (SessionDetails is null)
            {
                TempData["ErrorMessage"] = "Session is Not Found";
                return RedirectToAction(nameof(Index));

            }
            return View(SessionDetails);
        }

        public ActionResult Create()
        {
            LoadDataForCategories();
            LoadDataForTrainer();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createSession)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("IvaildData", "Check Data and Missing fields");
                LoadDataForCategories();
                LoadDataForTrainer();
                return View(nameof(Create), createSession);

            }
            var CreateSession = sessionServices.CreateSession(createSession);
            if (CreateSession)
            {
                TempData["SucessMessage"] = "Session Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Not Created";
            }
            return RedirectToAction(nameof(Index));
        }


        #region Edit
        public ActionResult Edit(int id)
        {
            if(id < 0)
            {
                TempData["ErrorMessage"] = "Invaild Session Id";
                return RedirectToAction(nameof(Index));
            }
            var Session = sessionServices.GetSessionToUpdate(id);
            if(Session is null)
            {
                TempData["ErrorMessage"] = "Can not Updated";
                return RedirectToAction(nameof(Index));

            }
            LoadDataForTrainer();
            return View(Session);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute]int id, UpdateSessionViewModel updateSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDataForTrainer();
                return View(updateSession);
            }
            var Update = sessionServices.UpdateSession(updateSession,id);
            if (Update)
            {
                TempData["SucessMessage"] = "Update Sucessfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Can not Update";
            }
            return RedirectToAction(nameof(Index));
        }









        #endregion


        #region Delete
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invaild Session Id";
                return RedirectToAction(nameof(Index));
            }
            var Session = sessionServices.GetById(id);
            if(Session is null)
            {
                TempData["ErrorMessage"] = "Session Can not Delete";
                return RedirectToAction(nameof(Index));
            }
            return View(Session);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = sessionServices.RemoveSession(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Can not Delete";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Helper
        private void LoadDataForTrainer()
        {
           
            var Trainers = sessionServices.GetTrainerDropList();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }
        private void LoadDataForCategories()
        {
            var Categorries = sessionServices.GetCategoryDropList();
            ViewBag.Categories = new SelectList(Categorries, "Id", "Name");
        
        }
        #endregion
    }
}
