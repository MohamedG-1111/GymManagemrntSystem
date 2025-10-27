namespace GymManagemrntSystem.Controllers
{
    using GymManagementSystemBLL.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="HomeController"/>
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Defines the analyticalService
        /// </summary>
        private readonly IAnalyticalService analyticalService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/>
        /// class.
        /// </summary>
        /// <param name="analyticalService">The
        ///     analyticalService<see cref="IAnalyticalService"/></param>
        public HomeController(IAnalyticalService analyticalService)
        {
            this.analyticalService = analyticalService;
        }

        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="IActionResult"/></returns>
        public IActionResult Index()
        {
            var Data = analyticalService.GetAnalytical();
            return View(Data);
        }
    }
}
