

namespace PMOS.DTO
{
    /// <summary>
    /// Задача проекта.
    /// </summary>
    public class ProjectTaskDTO
    {
        #region Свойства
        #region Идентификатор связи проекта и задачи.
        /// <summary>
        /// Идентификатор связи проекта и задачи.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region ID проекта.
        /// <summary>
        /// ID проекта.
        /// </summary>
        public int IdProject { get; set; }
        #endregion

        #region ID задачи.
        /// <summary>
        /// ID задачи.
        /// </summary>
        public int IdTask { get; set; }
        #endregion
        #endregion
    }
}
