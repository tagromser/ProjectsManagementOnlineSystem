

namespace PMOS.DTO
{
    /// <summary>
    /// Работник проекта.
    /// </summary>
    public class ProjectWorkerDTO
    {
        #region Свойства
        #region Идентификатор связи проекта и работника.
        /// <summary>
        /// Идентификатор связи проекта и работника.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region ID проекта.
        /// <summary>
        /// ID проекта.
        /// </summary>
        public int IdProject { get; set; }
        #endregion

        #region ID работника.
        /// <summary>
        /// ID работника.
        /// </summary>
        public int IdWorker { get; set; }
        #endregion
        #endregion
    }
}
