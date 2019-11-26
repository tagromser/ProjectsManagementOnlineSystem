using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMOS.Logic.Interfaces;

namespace PMOS.UI.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        public HomeController(IProjectManagementLogic projectManagementLogic)
        {
            _projectManagementLogic = projectManagementLogic;
        }
        #endregion

        #region Локальные переменные
        private readonly IProjectManagementLogic _projectManagementLogic;
        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}