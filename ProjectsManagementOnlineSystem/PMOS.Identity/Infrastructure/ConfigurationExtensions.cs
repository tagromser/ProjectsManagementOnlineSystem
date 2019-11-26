using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PMOS.Identity.Managers;
using PMOS.Identity.Stores;
using PMOS.DTO.Account;

namespace PMOS.Identity.Infrastructure
{
    /// <summary>
    /// Методы расширения для классов конфигурации.
    /// </summary>
    public static class ConfigurationExtensions
    {
        #region Конфигурирует систему идентификации.
        /// <summary>
        /// Конфигурирует систему идентификации.
        /// </summary>
        /// <param name="services">Задает контракт для набора дескрипторов услуг.</param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            //Добавляет конфигурацию системы идентификации по умолчанию для указанных типов пользователей и ролей.
            services.AddIdentity<UserDTO, RoleDTO>().AddDefaultTokenProviders().AddErrorDescriber<CustomIdentityErrorDescriber>(); // Локализация серверных ошибок;

            services.AddTransient<UserManager>();
            services.AddTransient<IUserStore<UserDTO>, UserStore<UserDTO>>();
            services.AddTransient<IRoleStore<RoleDTO>, RoleStore<RoleDTO>>();
        }
        #endregion
    }
}
