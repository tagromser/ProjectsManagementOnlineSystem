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
            this.projectManagementLogic = projectManagementLogic;
        }
        #endregion

        #region Локальные переменные
        /// <summary>
        /// Предоставляет API для управления проектами.
        /// </summary>
        private readonly IProjectManagementLogic projectManagementLogic;
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}