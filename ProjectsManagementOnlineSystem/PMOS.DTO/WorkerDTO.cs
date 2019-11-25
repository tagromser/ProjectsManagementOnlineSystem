

namespace PMOS.DTO
{
    /// <summary>
    /// Работник.
    /// </summary>
    public class WorkerDTO
    {
        #region Свойства
        #region Идентификатор работника.
        /// <summary>
        /// Идентификатор работника.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region ID пользователя.
        /// <summary>
        /// ID пользователя.
        /// </summary>
        public int IdUser { get; set; }
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
        #endregion
    }
}
