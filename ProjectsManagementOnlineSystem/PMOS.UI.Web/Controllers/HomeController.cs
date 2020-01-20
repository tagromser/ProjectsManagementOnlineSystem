using AutoMapper;
using CCFI.UI.Web.Controllers.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMOS.Logic.Interfaces;

namespace PMOS.UI.Web.Controllers
{
    [Authorize]
    public class HomeController : GenericController
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="projectManagementLogic">Предоставляет API для управления проектами.</param>
        /// <param name="mapper">Маппер для маппинга объектов.</param>
        public HomeController(IProjectManagementLogic projectManagementLogic, IMapper mapper) : base(mapper)
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