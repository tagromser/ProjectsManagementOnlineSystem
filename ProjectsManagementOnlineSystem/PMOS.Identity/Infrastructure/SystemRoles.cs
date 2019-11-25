

namespace PMOS.Identity.Infrastructure
{
    /// <summary>
    /// Системные роли.
    /// </summary>
    public enum SystemRoles
    {
        #region Руководитель.
        /// <summary>
        /// Руководитель.
        /// </summary>
        //[StringValue("FORMS")]
        Supervisor = 1,
        #endregion

        #region Менеджер проекта.
        /// <summary>
        /// Менеджер проекта.
        /// </summary>
        ProjectManager = 2,
        #endregion

        #region Сотрудник.
        /// <summary>
        /// Сотрудник.
        /// </summary>
        Employee = 3
        #endregion
    }
}