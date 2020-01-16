using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMOS.DataAccess.Model.PMOS.Physical
{
    /// <summary>
    /// Статус.
    /// </summary>
    [Table("Status", Schema = "PMOS")]
    public class Status
    {
        #region Свойства
        #region Идентификатор статуса.
        /// <summary>
        /// Идентификатор статуса.
        /// </summary>
        [Key, Column("ID"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region Имя статуса.
        /// <summary>
        /// Имя статуса.
        /// </summary>
        [Column("Name"), Required]
        public string Name { get; set; }
        #endregion
        #endregion
    }
}
