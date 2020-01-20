namespace PMOS.DTO.Account
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class UserDTO
    {
        #region Свойства
        #region Идентификатор пользователя.
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Имя/ник пользователя.
        /// <summary>
        /// Имя/ник пользователя.
        /// </summary>
        public string UserName { get; set; }
        #endregion

        #region Хэш пароля.
        /// <summary>
        /// Хэш пароля.
        /// </summary>
        public string PasswordHash { get; set; }
        #endregion

        #region Некоторое значение, которое меняется при каждой смене настроек аутентификации для данного пользователя.
        /// <summary>
        /// Некоторое значение, которое меняется при каждой смене настроек аутентификации для данного пользователя.
        /// </summary>
        public string SecurityStamp { get; set; }
        #endregion
        #endregion
    }
}
