using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMOS.DTO;
using PMOS.Identity.Managers;
using PMOS.UI.Web.Models.Worker;

namespace PMOS.UI.Web.Controllers
{
    [Authorize]
    public class WorkerController : Controller
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userManager">Предоставляет API для управления пользователями в хранилище.</param>
        public WorkerController(UserManager userManager)
        {
            _userManager = userManager;
        }
        #endregion

        #region Локальные переменные
        /// <summary>
        /// Предоставляет API для управления пользователями в хранилище.
        /// </summary>
        private readonly UserManager _userManager;
        #endregion

        #region Отображает страницу с работниками. Метод: "GET". Адрес: "/Worker/Index".
        /// <summary>
        /// Отображает страницу с работниками. Метод: "GET". Адрес: "/Worker/Index".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<WorkerDTO> workers = await _userManager.GetWorkers();

            List<WorkerViewModel> view = new List<WorkerViewModel>();

            foreach(var worker in workers)
            {
                WorkerViewModel workerViewModel = new WorkerViewModel
                {
                    Id = worker.Id,
                    Name = worker.Name,
                    Surname = worker.Surname,
                    Patronymic = worker.Patronymic,
                    Email = worker.Email
                };

                view.Add(workerViewModel);
            }

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
            WorkerDTO worker = await _userManager.GetWorkerById(id);

            WorkerDetailsModel workerDetailsModel = new WorkerDetailsModel
            {
                Id = worker.Id,
                Name = worker.Name,
                Surname = worker.Surname,
                Patronymic = worker.Patronymic,
                Email = worker.Email
            };

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
            WorkerDTO worker = await _userManager.GetWorkerById(id);

            WorkerEditModel workerEditModel = new WorkerEditModel
            {
                Id = worker.Id,
                IdUser = worker.IdUser,
                Name = worker.Name,
                Surname = worker.Surname,
                Patronymic = worker.Patronymic,
                Email = worker.Email
            };

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

            WorkerDTO workerDTO = new WorkerDTO
            {
                Id = workerEditModel.Id,
                IdUser = workerEditModel.IdUser,
                Name = workerEditModel.Name,
                Surname = workerEditModel.Surname,
                Patronymic = workerEditModel.Patronymic,
                Email = workerEditModel.Email
            };

            var result = await _userManager.UpdateWorker(workerDTO);

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
            var result = await _userManager.DeleteWorker(id);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(WorkerController.Index), "Worker");
            }

            return View();
        }
        #endregion
    }
}