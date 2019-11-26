using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMOS.DataAccess.Model.PMOS.Tables;
using PMOS.DataAccess.Context;
using PMOS.DTO;
using PMOS.Logic.Interfaces;
using System;
using System.Linq;
using PMOS.Logic.Common;

namespace PMOS.Logic.Logic
{
    /// <summary>
    /// Реализация интерфейса логики управления проектов.
    /// </summary>
    public class ProjectManagementLogic : IProjectManagementLogic
    {
        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pmosContext">Контекст для работы с базой данных PMOS</param>
        public ProjectManagementLogic(PMOSContext pmosContext)
        {
            _pmosContext = pmosContext;
        }
        #endregion

        #region Локальные переменные
        #region Контекст базы данных.
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private PMOSContext _pmosContext;
        #endregion
        #endregion

        #region Получает список проектов.
        /// <summary>
        /// Получает список проектов.
        /// </summary>
        /// <returns>Список проектов.</returns>
        public async Task<IEnumerable<ProjectDTO>> GetProjects()
        {
            List<Project> projects = await _pmosContext.Projects.ToListAsync();

            return projects.Select(projectsDTO => new ProjectDTO
            {
                Id = projectsDTO.Id,
                Name = projectsDTO.Name,
                CustomerCompanyName = projectsDTO.CustomerCompanyName,
                PerformerCompanyName = projectsDTO.PerformerCompanyName,
                StartDate = projectsDTO.StartDate,
                EndDate = projectsDTO.EndDate,
                Priority = projectsDTO.Priority
            });
        }
        #endregion

        #region Получает информацию о проекте.
        /// <summary>
        /// Получает информацию о проекте.
        /// </summary>
        /// <returns>Информация о проекте.</returns>
        public async Task<ProjectDTO> GetProjectById(int id)
        {
            Project project = await _pmosContext.Projects.FirstOrDefaultAsync(item => item.Id == id);

            if (project == null)
                throw new ArgumentNullException(nameof(project));

            var query = from w in _pmosContext.Workers
                              join pw in _pmosContext.ProjectWorkers on w.Id equals pw.IdWorker
                              join u in _pmosContext.Users on w.IdUser equals u.Id
                              join ur in _pmosContext.UserRoles on u.Id equals ur.IdUser
                                where ur.IdRole == 1 && pw.IdProject.Equals(id)
                              select w.Id;

            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                CustomerCompanyName = project.CustomerCompanyName,
                PerformerCompanyName = project.PerformerCompanyName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                IdWorkerProject = query.First()
            };
        }
        #endregion

        #region Создание проекте.
        /// <summary>
        /// Создание проекте.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<OperationResult> CreateProject(ProjectDTO projectDTO)
        {
            if (projectDTO == null)
                throw new ArgumentNullException(nameof(projectDTO));

            using (var transaction = _pmosContext.Database.BeginTransaction())
            {
                try
                {
                    Project project = new Project
                    {
                        Id = projectDTO.Id,
                        Name = projectDTO.Name,
                        CustomerCompanyName = projectDTO.CustomerCompanyName,
                        PerformerCompanyName = projectDTO.PerformerCompanyName,
                        StartDate = projectDTO.StartDate,
                        EndDate = projectDTO.EndDate,
                        Priority = projectDTO.Priority
                    };

                    _pmosContext.Projects.Add(project);
                    await _pmosContext.SaveChangesAsync();

                    ProjectWorker projectWorker = new ProjectWorker
                    {
                        IdProject = project.Id,
                        IdWorker = projectDTO.IdWorkerProject
                    };

                    _pmosContext.ProjectWorkers.Add(projectWorker);
                    await _pmosContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return OperationResult.Failed;
                }
            }

            return OperationResult.Success;
        }
        #endregion

        #region Обновление информации о работнике.
        /// <summary>
        /// Обновление информации о работнике.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<OperationResult> UpdateProject(ProjectDTO projectDTO)
        {
            if (projectDTO == null)
                throw new ArgumentNullException(nameof(projectDTO));

            using (var transaction = _pmosContext.Database.BeginTransaction())
            {
                try
                {
                    Project project = new Project
                    {
                        Id = projectDTO.Id,
                        Name = projectDTO.Name,
                        CustomerCompanyName = projectDTO.CustomerCompanyName,
                        PerformerCompanyName = projectDTO.PerformerCompanyName,
                        StartDate = projectDTO.StartDate,
                        EndDate = projectDTO.EndDate,
                        Priority = projectDTO.Priority
                    };

                    _pmosContext.Projects.Update(project);
                    await _pmosContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return OperationResult.Failed;
                }
            }

            return OperationResult.Success;
        }
        #endregion

        #region Удаление проекта.
        /// <summary>
        /// Удаление проекта.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<OperationResult> DeleteProject(int id)
        {
            if (id == 0)
                return OperationResult.Failed;

            Project project = _pmosContext.Projects.FirstOrDefault(item => item.Id == id);
            var projectWorker = _pmosContext.ProjectWorkers.Where(item => item.IdProject == project.Id);

            using (var transaction = _pmosContext.Database.BeginTransaction())
            {
                try
                {
                    if (projectWorker != null)
                    {
                        _pmosContext.ProjectWorkers.RemoveRange(projectWorker);
                        await _pmosContext.SaveChangesAsync();
                    }

                    if (project != null)
                    {
                        _pmosContext.Projects.Remove(project);
                        await _pmosContext.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return OperationResult.Failed;
                }
            }

            return OperationResult.Success;
        }
        #endregion

        #region Получение рабочего по id пользователя.
        /// <summary>
        /// Получение рабочего по id пользователя.
        /// </summary>
        /// <returns>Рабочего</returns>
        public async Task<WorkerDTO> GetWorkerProjectByIdUser(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            Worker worker = await _pmosContext.Workers.FirstOrDefaultAsync(item => item.IdUser == id);

            return new WorkerDTO
            {
                Id = worker.Id,
                IdUser = worker.IdUser,
                Name = worker.Name,
                Surname = worker.Surname,
                Patronymic = worker.Patronymic,
                Email = worker.Email
            };
        }
        #endregion

        #region Получение рабочих по id проекта.
        /// <summary>
        /// Получение рабочих по id проекта.
        /// </summary>
        /// <returns>Список рабочих</returns>
        public async Task<List<WorkerDTO>> GetWorkersByIdProject(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            var query = await (from w in _pmosContext.Workers
                        join pw in _pmosContext.ProjectWorkers on w.Id equals pw.IdWorker
                        join u in _pmosContext.Users on w.IdUser equals u.Id
                        join ur in _pmosContext.UserRoles on u.Id equals ur.IdUser
                        where pw.IdProject == id && ur.IdRole != 1
                        select new WorkerDTO
                        {
                            Id = w.Id,
                            IdUser = w.IdUser,
                            Name = w.Name,
                            Surname = w.Surname,
                            Patronymic = w.Patronymic,
                            Email = w.Email,
                            IdProjectWorker = pw.Id
                        }).ToListAsync();

            return query;
        }
        #endregion

        #region Удаление рабочего с проекта.
        /// <summary>
        /// Удаление рабочего с проекта.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<OperationResult> DeleteProjectWorker(int idProjectWorker)
        {
            if (idProjectWorker == 0)
                return OperationResult.Failed;

            ProjectWorker projectWorker = _pmosContext.ProjectWorkers.FirstOrDefault(item => item.Id == idProjectWorker);

            using (var transaction = _pmosContext.Database.BeginTransaction())
            {
                try
                {
                    if (projectWorker != null)
                    {
                        _pmosContext.ProjectWorkers.Remove(projectWorker);
                        await _pmosContext.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return OperationResult.Failed;
                }
            }

            return OperationResult.Success;
        }
        #endregion

        #region Получает список работников для добавления к проекту.
        /// <summary>
        /// Получает список работников для добавления к проекту.
        /// </summary>
        /// <returns>Список работников.</returns>
        public async Task<IEnumerable<WorkerDTO>> GetWorkersForAdding(int idProject)
        {
            var query = await (from w in _pmosContext.Workers
                        join u in _pmosContext.Users on w.IdUser equals u.Id
                        join ur in _pmosContext.UserRoles on u.Id equals ur.IdUser
                        where ur.IdRole != 1
                        select new WorkerDTO
                        {
                            Id = w.Id,
                            IdUser = w.IdUser,
                            Name = w.Name,
                            Surname = w.Surname,
                            Patronymic = w.Patronymic,
                            Email = w.Email
                        }).ToListAsync();

            return query;
        }
        #endregion

        #region Добавление рабочего к проекту.
        /// <summary>
        /// Добавление рабочего к проекту.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<bool> AddProjectWorker(int idWorker, int idProject)
        {
            if (idWorker == 0 || idProject == 0)
                return false;

            ProjectWorker workerProject = await _pmosContext.ProjectWorkers.FirstOrDefaultAsync(item => item.IdWorker == idWorker && item.IdProject == idProject);

            if(workerProject != null)
                return false;

            using (var transaction = _pmosContext.Database.BeginTransaction())
            {
                try
                {
                    ProjectWorker projectWorker = new ProjectWorker
                    {
                        IdProject = idProject,
                        IdWorker = idWorker
                    };

                    _pmosContext.ProjectWorkers.Add(projectWorker);
                    await _pmosContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }

            return true;
        }
        #endregion

    }
}
