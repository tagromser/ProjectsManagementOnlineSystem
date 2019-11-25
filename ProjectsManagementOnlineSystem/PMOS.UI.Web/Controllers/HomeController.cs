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
            //int count = await _projectManagementLogic.GetProjects();

            //if (pageIndex != 1)
            //{
            //    if (pageIndex < 1)
            //        return RedirectToAction("Index", new { pageIndex = 1 });

            //    int totalPages = (int)Math.Ceiling(count / (double)PageSize);

            //    if (pageIndex > totalPages)
            //        return RedirectToAction("Index", new { pageIndex = totalPages });
            //}

            //var projects = await _projectManagementLogic.GetProject();

            //var items = projects.Select(project =>
            //    new ProjectViewModel
            //    {
            //        Id = project.Id,
            //        Name = project.Name
            //    }).ToList();

            //var pagination = new Pagination<ProjectViewModel>(items, count, pageIndex, PageSize);

            //return View(pagination);
            return View();
        }
    }
}