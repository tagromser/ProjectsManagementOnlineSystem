using System;

namespace PMOS.UI.Web.Models.Project
{
    /// <summary>
    /// Модель представляющая проект для отображения деталей.
    /// </summary>
    public class ProjectDetailsModel
    {
        #region Id проекта.
        /// <summary>
        /// Id проекта.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Название проекта.
        /// <summary>
        /// Название проекта.
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Название компании-заказчика.
        /// <summary>
        /// Название компании-заказчика.
        /// </summary>
        public string CustomerCompanyName { get; set; }
        #endregion

        #region Название компании-исполнителя.
        /// <summary>
        /// Название компании-исполнителя.
        /// </summary>
        public string PerformerCompanyName { get; set; }
        #endregion

        #region Дата начала проекта.
        /// <summary>
        /// Дата начала проекта.
        /// </summary>
        public DateTime StartDate { get; set; }
        #endregion

        #region Дата окончания проекта.
        /// <summary>
        /// Дата окончания проекта.
        /// </summary>
        public DateTime EndDate { get; set; }
        #endregion

        #region Приоритет проекта.
        /// <summary>
        /// Приоритет проекта.
        /// </summary>
        public int Priority { get; set; }
        #endregion

        #region ФИО руководителя проекта.
        /// <summary>
        /// ФИО руководителя проекта.
        /// </summary>
        public string FIOWorkerProject { get; set; }
        #endregion
    }
}
