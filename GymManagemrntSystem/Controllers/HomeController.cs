using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemDAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace GymManagemrntSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticalService analyticalService;

        public HomeController(IAnalyticalService analyticalService)
        {
            this.analyticalService = analyticalService;
        }
        public IActionResult Index()
        {
            var Data = analyticalService.GetAnalytical();
            return View(Data);
        }
      
      
        
    }
}
