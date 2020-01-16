﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMOS.DTO;
using PMOS.Logic.Interfaces;
using PMOS.UI.Web.Models.Project;
using PMOS.Identity.Managers;
using System.Linq;

namespace PMOS.UI.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="projectManagementLogic">Предоставляет API для управления проектами.</param>
        /// <param name="userManager">Предоставляет API для управления пользователями в хранилище.</param>
        public ProjectController(IProjectManagementLogic projectManagementLogic, UserManager userManager)
        {
            this.projectManagementLogic = projectManagementLogic;
            this.userManager = userManager;
        }
        #endregion

        #region Локальные переменные
        /// <summary>
        /// Предоставляет API для управления проектами.
        /// </summary>
        private readonly IProjectManagementLogic projectManagementLogic;

        /// <summary>
        /// Предоставляет API для управления пользователями в хранилище.
        /// </summary>
        private readonly UserManager userManager;
        #endregion

        #region Отображает страницу с проектами. Метод: "GET". Адрес: "/Project/Index".
        /// <summary>
        /// Отображает страницу с проектами. Метод: "GET". Адрес: "/Project/Index".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProjectDTO> projects = await projectManagementLogic.GetProjects();

            List<ProjectViewModel> view = projects.Select(project => new ProjectViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                CustomerCompanyName = project.CustomerCompanyName,
                PerformerCompanyName = project.PerformerCompanyName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority
            }).ToList();

            return View(view);
        }
        #endregion

        #region Отображает страницу создания проекта. Метод: "GET". Адрес: "/Project/Create".
        /// <summary>
        /// Отображает страницу создания проекта. Метод: "GET". Адрес: "/Project/Create".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Осуществляет создание проекта. Метод: "POST". Адрес: "/Project/Create".
        /// <summary>
        /// Осуществляет создание проекта. Метод: "POST". Адрес: "/Project/Create".
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCreateModel projectCreateModel)
        {
            if (projectCreateModel == null)
                throw new ArgumentNullException(nameof(projectCreateModel));

            int idUser = Convert.ToInt32(userManager.GetUserId(User));

            WorkerDTO workerDTO = projectManagementLogic.GetWorkerProjectByIdUser(idUser);

            ProjectDTO projectDTO = new ProjectDTO
            {
                Id = projectCreateModel.Id,
                Name = projectCreateModel.Name,
                CustomerCompanyName = projectCreateModel.CustomerCompanyName,
                PerformerCompanyName = projectCreateModel.PerformerCompanyName,
                StartDate = projectCreateModel.StartDate,
                EndDate = projectCreateModel.EndDate,
                Priority = projectCreateModel.Priority,
                IdWorkerProject = workerDTO.Id
            };

            var result = await projectManagementLogic.CreateProject(projectDTO);

            if (result)
            {
                return RedirectToAction(nameof(ProjectController.Index), "Project");
            }

            return View();
        }
        #endregion

        #region Отображает страницу работников проекта. Метод: "GET". Адрес: "/Project/ProjectWorkers".
        /// <summary>
        /// Отображает страницу работников проекта. Метод: "GET". Адрес: "/Project/ProjectWorkers".
        /// </summary>
        /// <param name="idProject">id проекта</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ProjectWorkers(int idProject)
        {
            List<WorkerDTO> workers = await projectManagementLogic.GetWorkersByIdProject(idProject);

            List<ProjectWorkerModel> view = workers.Select(worker => new ProjectWorkerModel
            {
                Id = worker.Id,
                Name = worker.Name,
                Surname = worker.Surname,
                Patronymic = worker.Patronymic,
                Email = worker.Email,
                IdProjectWorker = worker.IdProjectWorker
            }).ToList();

            ViewBag.IdProject = idProject;

            return View(view);
        }
        #endregion

        #region Отображает страницу добавления рабочего к проекту. Метод: "GET". Адрес: "/Project/AddProjectWorker".
        /// <summary>
        /// Отображает страницу добавления рабочего к проекту. Метод: "GET". Адрес: "/Project/AddProjectWorker".
        /// </summary>
        /// <param name="idProject">id проекта</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddProjectWorker(int idProject)
        {
            IEnumerable<WorkerDTO> workers = await projectManagementLogic.GetWorkersForAdding();

            List<ProjectWorkerModel> view = workers.Select(worker => new ProjectWorkerModel
            {
                Id = worker.Id,
                Name = worker.Name,
                Surname = worker.Surname,
                Patronymic = worker.Patronymic,
                Email = worker.Email
            }).ToList();

            ViewBag.IdProject = idProject;

            return View(view);
        }
        #endregion

        #region Осуществляет добавление рабочего к проекту. Метод: "GET". Адрес: "/Project/AddProjectWorkerAction".
        /// <summary>
        /// Осуществляет добавление рабочего к проекту. Метод: "GET". Адрес: "/Project/AddProjectWorkerAction".
        /// </summary>
        /// <param name="idWorker">id работника</param>
        /// <param name="idProject">id проекта</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddProjectWorkerAction(int idWorker, int idProject)
        {
            var result = await projectManagementLogic.AddProjectWorker(idWorker, idProject);

            if (result == true)
            {
                return RedirectToAction(nameof(ProjectController.ProjectWorkers), "Project", new { idProject });
            }

            return RedirectToAction(nameof(ProjectController.AddProjectWorker), "Project", new { idProject });
        }
        #endregion

        #region Осуществляет удаление рабочего с проекта. Метод: "GET". Адрес: "/Project/DeleteProjectWorker".
        /// <summary>
        /// Осуществляет удаление рабочего с проекта. Метод: "GET". Адрес: "/Project/DeleteProjectWorker".
        /// </summary>
        /// <param name="idProjectWorker">id связки работника с проектом</param>
        /// <param name="idProject">id проекта</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeleteProjectWorker(int idProjectWorker, int idProject)
        {
            var result = await projectManagementLogic.DeleteProjectWorker(idProjectWorker);

            if (result)
            {
                return RedirectToAction(nameof(ProjectController.ProjectWorkers), "Project", new { idProject });
            }

            return View();
        }
        #endregion

        #region Отображает страницу деталей проекта. Метод: "GET". Адрес: "/Project/Details".
        /// <summary>
        /// Отображает страницу деталей проекта. Метод: "GET". Адрес: "/Project/Details".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProjectDTO project = await projectManagementLogic.GetProjectById(id);

            WorkerDTO worker = await userManager.GetWorkerById(project.IdWorkerProject);

            ProjectDetailsModel projectDetailsModel = new ProjectDetailsModel
            {
                Id = project.Id,
                Name = project.Name,
                CustomerCompanyName = project.CustomerCompanyName,
                PerformerCompanyName = project.PerformerCompanyName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                FIOWorkerProject = String.Join(" ",worker.Surname,worker.Name,worker.Patronymic)
            };

            return View(projectDetailsModel);
        }
        #endregion

        #region Отображает страницу редактирования проекта. Метод: "GET". Адрес: "/Project/Edit".
        /// <summary>
        /// Отображает страницу редактирования проекта. Метод: "GET". Адрес: "/Project/Edit".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProjectDTO project = await projectManagementLogic.GetProjectById(id);

            ProjectEditModel projectEditModel = new ProjectEditModel
            {
                Id = project.Id,
                Name = project.Name,
                CustomerCompanyName = project.CustomerCompanyName,
                PerformerCompanyName = project.PerformerCompanyName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority
            };

            ViewBag.PriorityList = new List<int> { 1, 2, 3 };

            return View(projectEditModel);
        }
        #endregion

        #region Осуществляет редактирование проекта. Метод: "POST". Адрес: "/Project/Edit".
        /// <summary>
        /// Осуществляет редактирование проекта. Метод: "POST". Адрес: "/Project/Edit".
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectEditModel projectEditModel)
        {
            if (projectEditModel == null)
                throw new ArgumentNullException(nameof(projectEditModel));

            ProjectDTO projectDTO = new ProjectDTO
            {
                Id = projectEditModel.Id,
                Name = projectEditModel.Name,
                CustomerCompanyName = projectEditModel.CustomerCompanyName,
                PerformerCompanyName = projectEditModel.PerformerCompanyName,
                StartDate = projectEditModel.StartDate,
                EndDate = projectEditModel.EndDate,
                Priority = projectEditModel.Priority
            };

            var result = await projectManagementLogic.UpdateProject(projectDTO);

            if (result)
            {
                return RedirectToAction(nameof(ProjectController.Index), "Project");
            }

            return View();
        }
        #endregion

        #region Осуществляет удаление проекта. Метод: "GET". Адрес: "/Project/Delete".
        /// <summary>
        /// Осуществляет удаление проекта. Метод: "GET". Адрес: "/Project/Delete".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await projectManagementLogic.DeleteProject(id);

            if (result)
            {
                return RedirectToAction(nameof(ProjectController.Index), "Project");
            }

            return View();
        }
        #endregion
    }
}