using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMOS.DataAccess.Context;
using PMOS.Logic.Logic;
using PMOS.Logic.Interfaces;
using PMOS.DataAccess.Interfaces;
using PMOS.DataAccess;
using AutoMapper;

namespace PMOS.Logic.Infrastructure
{
    /// <summary>
    /// Методы расширения для классов конфигурации.
    /// </summary>
    public static class ConfigurationExtensions
    {
        #region Конфигурирует контекст базы данных.
        /// <summary>
        /// Конфигурирует контекст базы данных.
        /// </summary>
        /// <param name="services">Задает контракт для набора дескрипторов услуг.</param>
        /// <param name="configuration">Представляет набор свойств конфигурации приложения ключ / значение.</param>
        /// <param name="connectionStringName">Имя строки соединения.</param>
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
        {
            // Получаем строку подключения из файла конфигурации
            string connectionString = configuration.GetConnectionString(connectionStringName);

            // Добавляем контекст PMOSContext в качестве сервиса в приложение
            services.AddDbContext<PMOSContext>(options => options.UseSqlServer(connectionString));
            // Добавляем временный тип сервиса
            services.AddTransient<IStorage, Storage>();
        }
        #endregion

        public static void ConfigureLogic(this IServiceCollection services)
        {
            services.AddTransient<IProjectManagementLogic, ProjectManagementLogic>();
        }
    }
}