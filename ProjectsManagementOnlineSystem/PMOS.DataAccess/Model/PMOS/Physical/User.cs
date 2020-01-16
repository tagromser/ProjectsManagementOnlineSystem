using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PMOS.DataAccess.Model.PMOS.Physical
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [Table("User", Schema = "PMOS")]
    public class User
    {
        #region Свойства
        #region Идентификатор пользователя.
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [Key, Column("ID"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region Имя/ник пользователя.
        /// <summary>
        /// Имя/ник пользователя.
        /// </summary>
        [Column("UserName"), Required]
        public string UserName { get; set; }
        #endregion

        #region Хэш пароля.
        /// <summary>
        /// Хэш пароля.
        /// </summary>
        [Column("PasswordHash")]
        public string PasswordHash { get; set; }
        #endregion

        #region Некоторое значение, которое меняется при каждой смене настроек аутентификации для данного пользователя.
        /// <summary>
        /// Некоторое значение, которое меняется при каждой смене настроек аутентификации для данного пользователя.
        /// </summary>
        [Column("SecurityStamp")]
        public string SecurityStamp { get; set; }
        #endregion
        #endregion
    }
}