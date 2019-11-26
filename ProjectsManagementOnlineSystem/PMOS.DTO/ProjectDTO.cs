using System;

namespace PMOS.DTO
{
    /// <summary>
    /// Проект.
    /// </summary>
    public class ProjectDTO
    {
        #region Свойства
        #region Идентификатор проекта.
        /// <summary>
        /// Идентификатор проекта.
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

        #region ID руководителя проекта.
        /// <summary>
        /// ID руководителя проекта.
        /// </summary>
        public int IdWorkerProject { get; set; }
        #endregion
        #endregion
    }
}
