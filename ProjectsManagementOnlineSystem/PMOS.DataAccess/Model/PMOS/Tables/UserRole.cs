using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMOS.DataAccess.Model.PMOS.Tables
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    [Table("UserRole", Schema = "PMOS")]
    public class UserRole
    {
        #region Свойства
        #region Идентификатор связи роли и пользователя.
        /// <summary>
        /// Идентификатор связи роли и пользователя.
        /// </summary>
        [Key, Column("ID"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region ID пользователя.
        /// <summary>
        /// ID пользователя.
        /// </summary>
        [Column("ID_User"), Required, ForeignKey(nameof(User))]
        public int IdUser { get; set; }
        #endregion

        #region ID роли.
        /// <summary>
        /// ID работника.
        /// </summary>
        [Column("ID_Role"), Required, ForeignKey(nameof(Role))]
        public int IdRole { get; set; }
        #endregion
        #endregion
    }
}
