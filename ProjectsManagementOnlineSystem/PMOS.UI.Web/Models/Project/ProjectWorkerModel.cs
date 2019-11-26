using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMOS.UI.Web.Models.Project
{
    /// <summary>
    /// Модель представляющая рабочих проекта.
    /// </summary>
    public class ProjectWorkerModel
    {
        #region Id работника.
        /// <summary>
        /// Id работника.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Имя работника.
        /// <summary>
        /// Имя работника.
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Фамилия работника.
        /// <summary>
        /// Фамилия работника.
        /// </summary>
        public string Surname { get; set; }
        #endregion

        #region Отчество работника.
        /// <summary>
        /// Отчество работника.
        /// </summary>
        public string Patronymic { get; set; }
        #endregion

        #region Email работника.
        /// <summary>
        /// Email работника.
        /// </summary>
        public string Email { get; set; }
        #endregion

        #region id связки работника и проекта.
        /// <summary>
        /// id связки работника и проекта.
        /// </summary>
        public int IdProjectWorker { get; set; }
        #endregion
    }
}
