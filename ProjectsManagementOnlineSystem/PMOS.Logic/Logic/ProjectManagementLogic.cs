using System.Collections.Generic;
using System.Threading.Tasks;
using PMOS.DTO;
using PMOS.Logic.Interfaces;
using System;
using System.Linq;
using CCFI.Logic.Logics;
using PMOS.DataAccess.Interfaces;
using AutoMapper;
using PMOS.DataAccess.Model.PMOS.Physical;

namespace PMOS.Logic.Logic
{
    /// <summary>
    /// Реализация интерфейса логики управления проектов.
    /// </summary>
    public class ProjectManagementLogic : ManagementLogic, IProjectManagementLogic
    {
        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pmosContext">Контекст для работы с базой данных PMOS</param>
        public ProjectManagementLogic(IStorage storage, IMapper mapper) : base(storage, mapper)
        {
        }
        #endregion

        #region Получает список проектов.
        /// <summary>
        /// Получает список проектов.
        /// </summary>
        /// <returns>Список проектов.</returns>
        public async Task<IEnumerable<ProjectDTO>> GetProjects()
        {
            IEnumerable<Project> projects = await storage.GetRepository<Project>().GetAll();

            return projects.Select(project => mapper.Map<Project, ProjectDTO>(project, options => options.ConfigureMap()
                .ForMember(destinationMember => destinationMember.IdWorkerProject, opt => opt.Ignore()))).ToList();
        }
        #endregion

        #region Получает информацию о проекте.
        /// <summary>
        /// Получает информацию о проекте.
        /// </summary>
        /// <returns>Информация о проекте.</returns>
        public async Task<ProjectDTO> GetProjectById(int id)
        {
            Project project = await storage.GetRepository<Project>().FindById(id);

            if (project == null)
                throw new ArgumentNullException(nameof(project));

            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                CustomerCompanyName = project.CustomerCompanyName,
                PerformerCompanyName = project.PerformerCompanyName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                IdWorkerProject = await storage.VirtualRepository.GetWorkerIdByProjectId(project.Id)
            };
        }
        #endregion

        #region Создание проекте.
        /// <summary>
        /// Создание проекте.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<bool> CreateProject(ProjectDTO projectDTO)
        {
            if (projectDTO == null)
                throw new ArgumentNullException(nameof(projectDTO));

            using (var transaction = storage.BeginTransaction())
            {
                try
                {
                    Project project = mapper.Map<Project>(projectDTO);
                    await storage.GetRepository<Project>().Create(project);

                    ProjectWorker projectWorker = new ProjectWorker
                    {
                        IdProject = project.Id,
                        IdWorker = projectDTO.IdWorkerProject
                    };
                    await storage.GetRepository<ProjectWorker>().Create(projectWorker);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

            return true;
        }
        #endregion

        #region Обновление информации о работнике.
        /// <summary>
        /// Обновление информации о работнике.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<bool> UpdateProject(ProjectDTO projectDTO)
        {
            if (projectDTO == null)
                throw new ArgumentNullException(nameof(projectDTO));

            using (var transaction = storage.BeginTransaction())
            {
                try
                {
                    Project project = mapper.Map<Project>(projectDTO);
                    await storage.GetRepository<Project>().Update(project);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

            return true;
        }
        #endregion

        #region Удаление проекта.
        /// <summary>
        /// Удаление проекта.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<bool> DeleteProject(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            Project project = await storage.GetRepository<Project>().FindById(id);
            IEnumerable<ProjectWorker> projectWorker = storage.GetRepository<ProjectWorker>().Get(item => item.IdProject == project.Id);

            using (var transaction = storage.BeginTransaction())
            {
                try
                {
                    if (projectWorker != null)
                        await storage.GetRepository<ProjectWorker>().DeleteRange(projectWorker);

                    if (project != null)
                        await storage.GetRepository<Project>().Delete(project); 

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

            return true;
        }
        #endregion

        #region Получение рабочего по id пользователя.
        /// <summary>
        /// Получение рабочего по id пользователя.
        /// </summary>
        /// <returns>Рабочего</returns>
        public WorkerDTO GetWorkerProjectByIdUser(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            IEnumerable<Worker> worker = storage.GetRepository<Worker>().Get(item => item.IdUser == id);

            return mapper.Map<Worker, WorkerDTO>(worker.FirstOrDefault(), options => options.ConfigureMap()
                .ForMember(destinationMember => destinationMember.RoleName, opt => opt.Ignore())
                .ForMember(destinationMember => destinationMember.IdProjectWorker, opt => opt.Ignore()));
        }
        #endregion

        #region Получение рабочих по id проекта.
        /// <summary>
        /// Получение рабочих по id проекта.
        /// </summary>
        /// <returns>Список рабочих</returns>
        public async Task<List<WorkerDTO>> GetWorkersByIdProject(int idProject)
        {
            if (idProject == 0)
                throw new ArgumentNullException(nameof(idProject));

            var workers = await storage.VirtualRepository.GetWorkersByProjectId(idProject);

            return mapper.Map<List<WorkerDTO>>(workers);
        }
        #endregion

        #region Удаление рабочего с проекта.
        /// <summary>
        /// Удаление рабочего с проекта.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<bool> DeleteProjectWorker(int idProjectWorker)
        {
            if (idProjectWorker == 0)
                throw new ArgumentNullException(nameof(idProjectWorker));

            ProjectWorker projectWorker = await storage.GetRepository<ProjectWorker>().FindById(idProjectWorker);

            if (projectWorker != null)
                throw new ArgumentNullException(nameof(projectWorker));

            using (var transaction = storage.BeginTransaction())
            {
                try
                {
                    await storage.GetRepository<ProjectWorker>().Delete(projectWorker);
 
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

            return true;
        }
        #endregion

        #region Получает список работников для добавления к проекту.
        /// <summary>
        /// Получает список работников для добавления к проекту.
        /// </summary>
        /// <returns>Список работников.</returns>
        public async Task<IEnumerable<WorkerDTO>> GetWorkersForAdding()
        {
            var workers = await storage.VirtualRepository.GetWorkersForAdding();

            return mapper.Map<List<WorkerDTO>>(workers);
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

            ProjectWorker workerProject =
                storage.GetRepository<ProjectWorker>().Get(item => item.IdWorker == idWorker && item.IdProject == idProject).FirstOrDefault();

            if (workerProject != null)
                return false;

            using (var transaction = storage.BeginTransaction())
            {
                try
                {
                    ProjectWorker projectWorker = new ProjectWorker
                    {
                        IdProject = idProject,
                        IdWorker = idWorker
                    };

                    await storage.GetRepository<ProjectWorker>().Create(projectWorker);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

            return true;
        }
        #endregion

    }
}
