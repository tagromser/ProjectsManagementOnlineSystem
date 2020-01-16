using PMOS.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMOS.Logic.Interfaces
{
    /// <summary>
    /// Интерфейс логики управления проектом.
    /// </summary>
    public interface IProjectManagementLogic
    {
        #region Получает список проектов.
        /// <summary>
        /// Получает список проектов.
        /// </summary>
        /// <returns>Список проектов.</returns>
        Task<IEnumerable<ProjectDTO>> GetProjects();
        #endregion

        #region Получает информацию о проекте.
        /// <summary>
        /// Получает информацию о проекте.
        /// </summary>
        /// <returns>Информация о проекте.</returns>
        Task<ProjectDTO> GetProjectById(int id);
        #endregion

        #region Создание проекте.
        /// <summary>
        /// Создание проекте.
        /// </summary>
        /// <returns>Результат</returns>
        Task<bool> CreateProject(ProjectDTO projectDTO);
        #endregion

        #region Обновление информации о работнике.
        /// <summary>
        /// Обновление информации о работнике.
        /// </summary>
        /// <returns>Результат</returns>
        Task<bool> UpdateProject(ProjectDTO projectDTO);
        #endregion

        #region Удаление проекта.
        /// <summary>
        /// Удаление проекта.
        /// </summary>
        /// <returns>Результат</returns>
        Task<bool> DeleteProject(int id);
        #endregion

        #region Получение рабочего по id пользователя.
        /// <summary>
        /// Получение рабочего по id пользователя.
        /// </summary>
        /// <returns>Рабочего</returns>
        WorkerDTO GetWorkerProjectByIdUser(int id);
        #endregion

        #region Получение рабочих по id проекта.
        /// <summary>
        /// Получение рабочих по id проекта.
        /// </summary>
        /// <returns>Список рабочих</returns>
        Task<List<WorkerDTO>> GetWorkersByIdProject(int id);
        #endregion

        #region Удаление рабочего с проекта.
        /// <summary>
        /// Удаление рабочего с проекта.
        /// </summary>
        /// <returns>Результат</returns>
        Task<bool> DeleteProjectWorker(int idProjectWorker);
        #endregion

        #region Получает список работников для добавления к проекту.
        /// <summary>
        /// Получает список работников для добавления к проекту.
        /// </summary>
        /// <returns>Список работников.</returns>
        Task<IEnumerable<WorkerDTO>> GetWorkersForAdding();
        #endregion

        #region Добавление рабочего к проекту.
        /// <summary>
        /// Добавление рабочего к проекту.
        /// </summary>
        /// <returns>Результат</returns>
        Task<bool> AddProjectWorker(int idWorker, int idProject);
        #endregion
    }
}
