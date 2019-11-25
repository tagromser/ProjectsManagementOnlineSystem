using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMOS.DataAccess.Model.PMOS.Tables
{
    /// <summary>
    /// Роль.
    /// </summary>
    [Table("Role", Schema = "PMOS")]
    public class Role
    {
        #region Свойства
        #region Идентификатор роли.
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        [Key, Column("ID"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region Имя роли.
        /// <summary>
        /// Имя роли.
        /// </summary>
        [Column("Name"), Required]
        public string Name { get; set; }
        #endregion
        #endregion
    }
}
