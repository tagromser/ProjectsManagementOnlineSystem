using Microsoft.EntityFrameworkCore;
using PMOS.DataAccess.Model.PMOS.Physical;

namespace PMOS.DataAccess.Context
{
    /// <summary>
    /// Контекст для работы с таблицами.
    /// </summary>
    public sealed partial class PMOSContext : DbContext
    {
        #region Таблицы
        #region Значения характеристик проектов.
        /// <summary>
        /// Значения характеристик проектов.
        /// </summary>
        public DbSet<Project> Projects { get; set; }
        #endregion

        #region Значения характеристик задач проекта.
        /// <summary>
        /// Значения характеристик задач проекта.
        /// </summary>
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        #endregion

        #region Значения характеристик работников проекта.
        /// <summary>
        /// Значения характеристик работников проекта.
        /// </summary>
        public DbSet<ProjectWorker> ProjectWorkers { get; set; }
        #endregion

        #region Значения характеристик ролей.
        /// <summary>
        /// Значения характеристик ролей.
        /// </summary>
        public DbSet<Role> Roles { get; set; }
        #endregion

        #region Значения характеристик статусов.
        /// <summary>
        /// Значения характеристик статусов.
        /// </summary>
        public DbSet<Status> Statuses { get; set; }
        #endregion

        #region Значения характеристик задач.
        /// <summary>
        /// Значения характеристик задач.
        /// </summary>
        public DbSet<Task> Tasks { get; set; }
        #endregion

        #region Значения характеристик пользователей.
        /// <summary>
        /// Значения характеристик пользователей.
        /// </summary>
        public DbSet<User> Users { get; set; }
        #endregion

        #region Значения характеристик ролей пользователя.
        /// <summary>
        /// Значения характеристик ролей пользователя.
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        #region Значения характеристик работников.
        /// <summary>
        /// Значения характеристик работников.
        /// </summary>
        public DbSet<Worker> Workers { get; set; }
        #endregion

        #region Значения характеристик задач работника.
        /// <summary>
        /// Значения характеристик задач работника.
        /// </summary>
        public DbSet<WorkerTask> WorkerTasks { get; set; }
        #endregion
        #endregion
    }
}
