using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMOS.DataAccess.Model.PMOS.Physical
{
    /// <summary>
    /// Задача.
    /// </summary>
    [Table("Task", Schema = "PMOS")]
    public class Task
    {
        #region Свойства
        #region Идентификатор задачи.
        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        [Key, Column("ID"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region Название задачи.
        /// <summary>
        /// Название задачи.
        /// </summary>
        [Column("Name"), Required]
        public string Name { get; set; }
        #endregion

        #region ID статуса.
        /// <summary>
        /// ID статуса.
        /// </summary>
        [Column("ID_Status"), Required, ForeignKey(nameof(Status))]
        public int IdStatus { get; set; }
        #endregion

        #region Комментарий.
        /// <summary>
        /// Комментарий.
        /// </summary>
        [Column("Comment")]
        public string Comment { get; set; }
        #endregion

        #region Приоритет задачи.
        /// <summary>
        /// Приоритет задачи.
        /// </summary>
        [Column("Priority"), Required]
        public int Priority { get; set; }
        #endregion
        #endregion
    }
}
