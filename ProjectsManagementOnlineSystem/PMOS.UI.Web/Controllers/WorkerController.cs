using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CCFI.UI.Web.Controllers.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMOS.DTO;
using PMOS.Identity.Managers;
using PMOS.UI.Web.Models.Worker;

namespace PMOS.UI.Web.Controllers
{
    [Authorize]
    public class WorkerController : GenericController
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userManager">Предоставляет API для управления пользователями в хранилище.</param>
        /// <param name="mapper">Маппер для маппинга объектов.</param>
        public WorkerController(UserManager userManager, IMapper mapper) : base(mapper)
        {
            this.userManager = userManager;
        }
        #endregion

        #region Локальные переменные
        /// <summary>
        /// Предоставляет API для управления пользователями в хранилище.
        /// </summary>
        private readonly UserManager userManager;
        #endregion

        #region Отображает страницу с работниками. Метод: "GET". Адрес: "/Worker/Index".
        /// <summary>
        /// Отображает страницу с работниками. Метод: "GET". Адрес: "/Worker/Index".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<WorkerDTO> workers = await userManager.GetWorkers();

            List<WorkerViewModel> view = mapper.Map<List<WorkerViewModel>>(workers);

            return View(view);
        }
        #endregion

        #region Отображает страницу создания работника. Метод: "GET". Адрес: "/Worker/Create".
        /// <summary>
        /// Отображает страницу создания работника. Метод: "GET". Адрес: "/Worker/Create".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Отображает страницу деталей работника. Метод: "GET". Адрес: "/Worker/Details".
        /// <summary>
        /// Отображает страницу деталей работника. Метод: "GET". Адрес: "/Worker/Details".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            WorkerDTO worker = await userManager.GetWorkerById(id);

            WorkerDetailsModel workerDetailsModel = mapper.Map<WorkerDetailsModel>(worker);

            return View(workerDetailsModel);
        }
        #endregion

        #region Отображает страницу редактирования работника. Метод: "GET". Адрес: "/Worker/Edit".
        /// <summary>
        /// Отображает страницу редактирования работника. Метод: "GET". Адрес: "/Worker/Edit".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            WorkerDTO worker = await userManager.GetWorkerById(id);

            WorkerEditModel workerEditModel = mapper.Map<WorkerEditModel>(worker);

            return View(workerEditModel);
        }
        #endregion

        #region Осуществляет редактирование работника. Метод: "POST". Адрес: "/Worker/Edit".
        /// <summary>
        /// Осуществляет редактирование работника. Метод: "POST". Адрес: "/Worker/Edit".
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WorkerEditModel workerEditModel)
        {
            if (workerEditModel == null)
                throw new ArgumentNullException(nameof(workerEditModel));

            WorkerDTO workerDTO = mapper.Map<WorkerEditModel, WorkerDTO>(workerEditModel, options => options.ConfigureMap()
                 .ForMember(destinationMember => destinationMember.RoleName, opt => opt.Ignore())
                 .ForMember(destinationMember => destinationMember.IdProjectWorker, opt => opt.Ignore()));

            var result = await userManager.UpdateWorker(workerDTO);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(WorkerController.Index), "Worker");
            }

            return View();
        }
        #endregion

        #region Осуществляет удаление работника. Метод: "GET". Адрес: "/Worker/Delete".
        /// <summary>
        /// Осуществляет удаление работника. Метод: "GET". Адрес: "/Worker/Delete".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await userManager.DeleteWorker(id);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(WorkerController.Index), "Worker");
            }

            return View();
        }
        #endregion
    }
}