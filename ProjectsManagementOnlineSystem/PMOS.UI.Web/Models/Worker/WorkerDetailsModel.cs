namespace PMOS.UI.Web.Models.Worker
{
    /// <summary>
    /// Модель представляющая работника для отображения деталей.
    /// </summary>
    public class WorkerDetailsModel
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

        #region Название роли работника.
        /// <summary>
        /// Название роли работника.
        /// </summary>
        public string RoleName { get; set; }
        #endregion
    }
}
