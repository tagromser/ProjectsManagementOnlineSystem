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

    }
}