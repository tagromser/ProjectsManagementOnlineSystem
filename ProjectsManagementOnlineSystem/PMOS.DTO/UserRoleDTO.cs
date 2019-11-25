

namespace PMOS.DTO
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public class UserRoleDTO
    {
        #region Свойства
        #region Идентификатор связи роли и пользователя.
        /// <summary>
        /// Идентификатор связи роли и пользователя.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region ID пользователя.
        /// <summary>
        /// ID пользователя.
        /// </summary>
        public int IdUser { get; set; }
        #endregion

        #region ID роли.
        /// <summary>
        /// ID работника.
        /// </summary>
        public int IdRole { get; set; }
        #endregion
        #endregion
    }
}
