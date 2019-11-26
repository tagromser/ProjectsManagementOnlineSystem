using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMOS.DataAccess.Context;
using PMOS.DataAccess.Model.PMOS.Tables;
using PMOS.DTO;
using PMOS.DTO.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Task = System.Threading.Tasks.Task;
using PMOS.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PMOS.Identity.Managers
{
    /// <summary>
    /// Предоставляет API для управления пользователем.
    /// </summary>
    public class UserManager : UserManager<UserDTO>
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="optionsAccessor"></param>
        /// <param name="passwordHasher"></param>
        /// <param name="userValidators"></param>
        /// <param name="passwordValidators"></param>
        /// <param name="keyNormalizer"></param>
        /// <param name="errors"></param>
        /// <param name="services"></param>
        /// <param name="logger"></param>
        /// <param name="pmosContext"></param>
        public UserManager(IUserStore<UserDTO> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserDTO> passwordHasher, IEnumerable<IUserValidator<UserDTO>> userValidators,
            IEnumerable<IPasswordValidator<UserDTO>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserDTO>> logger, PMOSContext pmosContext) : base(store,
            optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _pmosContext = pmosContext;
        }
        #endregion

        #region Локальные переменные
        #region Контекст для работы с базой данных.
        /// <summary>
        /// Контекст для работы с базой данных.
        /// </summary>
        private readonly PMOSContext _pmosContext;
        #endregion
        #endregion

        #region Создает пользователя.
        /// <summary>
        /// Создает пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="workerDTO">Информация о работнике.</param>
        /// <param name="idRole">ID роли.</param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(UserDTO user, string password, WorkerDTO workerDTO, int idRole)
        {
            var result = await CreateAsync(user, password);

            if (!result.Succeeded)
                return result;

            try
            {
                UserDTO userDTO = await FindByNameAsync(user.UserName);

                if (!await IsInRoleAsync(userDTO, Enum.GetName(typeof(SystemRoles), idRole)))
                {
                    _pmosContext.UserRoles.Add(new UserRole
                    {
                        IdUser = userDTO.Id,
                        IdRole = idRole
                    });
                }
                
                #region Добавляем информацию о работнике.
                var worker = new Worker
                {
                    IdUser = userDTO.Id,
                    Surname = workerDTO.Surname,
                    Name = workerDTO.Name,
                    Patronymic = workerDTO.Patronymic,
                    Email = workerDTO.Email
                };

                _pmosContext.Workers.Add(worker);

                await _pmosContext.SaveChangesAsync();
                #endregion
            }
            catch (Exception exception)
            {
                return await Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Description = exception.Message
                }));
            }

            return await Task.FromResult(IdentityResult.Success);
        }
        #endregion

        #region Получает список работников.
        /// <summary>
        /// Получает список работников.
        /// </summary>
        /// <returns>Список работников.</returns>
        public async Task<IEnumerable<WorkerDTO>> GetWorkers()
        {
            List<Worker> workers = await _pmosContext.Workers.ToListAsync();

            return workers.Select(workersDTO => new WorkerDTO
            {
                Id = workersDTO.Id,
                Name = workersDTO.Name,
                Surname = workersDTO.Surname,
                Patronymic = workersDTO.Patronymic,
                Email = workersDTO.Email
            });
        }
        #endregion

        #region Получает информацию о работнике.
        /// <summary>
        /// Получает информацию о работнике.
        /// </summary>
        /// <returns>Информация о работнике.</returns>
        public async Task<WorkerDTO> GetWorkerById(int id)
        {
            Worker worker = await _pmosContext.Workers.FirstOrDefaultAsync(item => item.Id == id);

            var role = (from u in _pmosContext.Users
                        join ur in _pmosContext.UserRoles on u.Id equals ur.IdUser
                        join r in _pmosContext.Roles on ur.IdRole equals r.Id
                        join w in _pmosContext.Workers on u.Id equals w.IdUser
                        where w.Id == id
                        select r.Name).FirstOrDefault();

            if (worker == null)
                throw new ArgumentNullException(nameof(worker));

            return new WorkerDTO
            {
                Id = worker.Id,
                IdUser = worker.IdUser,
                Name = worker.Name,
                Surname = worker.Surname,
                Patronymic = worker.Patronymic,
                Email = worker.Email,
                RoleName = role
            };
        }
        #endregion


        #region Обновление информации о работнике.
        /// <summary>
        /// Обновление информации о работнике.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<IdentityResult> UpdateWorker(WorkerDTO workerDTO)
        {
            if (workerDTO == null)
                throw new ArgumentNullException(nameof(workerDTO));

            Worker worker = new Worker
            {
                Id = workerDTO.Id,
                IdUser = workerDTO.IdUser,
                Name = workerDTO.Name,
                Surname = workerDTO.Surname,
                Patronymic = workerDTO.Patronymic,
                Email = workerDTO.Email
            };

            _pmosContext.Workers.Attach(worker);
            _pmosContext.Workers.Update(worker);

            try
            {
                await _pmosContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }
        #endregion


        #region Удаление работника.
        /// <summary>
        /// Удаление работника.
        /// </summary>
        /// <returns>Результат</returns>
        public async Task<IdentityResult> DeleteWorker(int id)
        {
            if (id == 0)
                return IdentityResult.Failed();

            Worker worker = _pmosContext.Workers.FirstOrDefault(item => item.Id == id);
            ProjectWorker projectWorker = _pmosContext.ProjectWorkers.FirstOrDefault(item => item.IdWorker == worker.Id);
            User user = _pmosContext.Users.FirstOrDefault(item => item.Id == worker.IdUser);
            UserRole userRole = _pmosContext.UserRoles.FirstOrDefault(item => item.IdUser == user.Id);
            UserDTO userDTO = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp
            };

            using (var transaction = _pmosContext.Database.BeginTransaction())
            {
                try
                {
                    if (projectWorker != null)
                    {
                        _pmosContext.ProjectWorkers.Remove(projectWorker);
                        await _pmosContext.SaveChangesAsync();
                    } 

                    if (worker != null)
                    {
                        _pmosContext.Workers.Remove(worker);
                        await _pmosContext.SaveChangesAsync();
                    }
                        
                    if (userRole != null)
                    {
                        _pmosContext.UserRoles.Remove(userRole);
                        await _pmosContext.SaveChangesAsync();
                    }

                    if (user != null)
                    {
                        _pmosContext.Users.Remove(user);
                        await _pmosContext.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return IdentityResult.Failed();
                }
            }

            return IdentityResult.Success;
        }
        #endregion

    }
}