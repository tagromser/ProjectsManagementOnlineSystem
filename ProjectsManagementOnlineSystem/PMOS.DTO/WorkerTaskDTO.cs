

namespace PMOS.DTO
{
    /// <summary>
    /// Задача работника.
    /// </summary>
    public class WorkerTaskDTO
    {
        #region Свойства
        #region Идентификатор связи работника и задачи.
        /// <summary>
        /// Идентификатор связи работника и задачи.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region ID работника.
        /// <summary>
        /// ID работника.
        /// </summary>
        public int IdWorker { get; set; }
        #endregion

        #region ID задачи.
        /// <summary>
        /// ID задачи.
        /// </summary>
        public int IdTask { get; set; }
        #endregion

        #region Является ли автором.
        /// <summary>
        /// Является ли автором.
        /// </summary>
        public bool IsAuthor { get; set; }
        #endregion
        #endregion
    }
}
