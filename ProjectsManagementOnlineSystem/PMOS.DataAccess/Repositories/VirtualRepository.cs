using Microsoft.EntityFrameworkCore;
using PMOS.DataAccess.Context;
using PMOS.DataAccess.Model.PMOS.Virtual;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMOS.DataAccess.Repositories
{
    /// <summary>
    /// Виртуальный репозиторий для работы с несколькими сущностями.
    /// </summary>
    public class VirtualRepository
    {
        private readonly PMOSContext pmosContext;

        public VirtualRepository(PMOSContext pmosContext)
        {
            this.pmosContext = pmosContext;
        }

        /// <summary>
        /// Получение ID руководителя проекта по ID проекта.
        /// </summary>
        /// <param name="idProject">ID проекта.</param>
        /// <returns>ID рабочего.</returns>
        public async Task<int> GetWorkerIdByProjectId(int idProject)
        {
            var query = await (from w in pmosContext.Workers
                        join pw in pmosContext.ProjectWorkers on w.Id equals pw.IdWorker
                        join u in pmosContext.Users on w.IdUser equals u.Id
                        join ur in pmosContext.UserRoles on u.Id equals ur.IdUser
                        where ur.IdRole == 1 && pw.IdProject.Equals(idProject)
                        select w.Id).ToAsyncEnumerable().ToList();

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Получения списка рабочих по ID проекта.
        /// </summary>
        /// <param name="idProject">ID проекта.</param>
        /// <returns>Список рабочих.</returns>
        public async Task<IEnumerable<WorkerView>> GetWorkersByProjectId(int idProject)
        {
            var query = await (from w in pmosContext.Workers
                               join pw in pmosContext.ProjectWorkers on w.Id equals pw.IdWorker
                               join u in pmosContext.Users on w.IdUser equals u.Id
                               join ur in pmosContext.UserRoles on u.Id equals ur.IdUser
                               where pw.IdProject == idProject && ur.IdRole != 1
                               select new WorkerView
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

        /// <summary>
        /// Получение списка рабочих для добавления в проект.
        /// </summary>
        /// <returns>Список рабочих.</returns>
        public async Task<IEnumerable<WorkerView>> GetWorkersForAdding()
        {
            var query = await (from w in pmosContext.Workers
                               join u in pmosContext.Users on w.IdUser equals u.Id
                               join ur in pmosContext.UserRoles on u.Id equals ur.IdUser
                               where ur.IdRole != 1
                               select new WorkerView
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
    }
}
