

namespace PMOS.DTO
{
    /// <summary>
    /// Задача.
    /// </summary>
    public class TaskDTO
    {
        #region Свойства
        #region Идентификатор задачи.
        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Название задачи.
        /// <summary>
        /// Название задачи.
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region ID статуса.
        /// <summary>
        /// ID статуса.
        /// </summary>
        public int IdStatus { get; set; }
        #endregion

        #region Комментарий.
        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment { get; set; }
        #endregion

        #region Приоритет задачи.
        /// <summary>
        /// Приоритет задачи.
        /// </summary>
        public int Priority { get; set; }
        #endregion
        #endregion
    }
}
