using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMOS.DataAccess.Model.PMOS.Physical
{
    /// <summary>
    /// Задача работника.
    /// </summary>
    [Table("WorkerTask", Schema = "PMOS")]
    public class WorkerTask
    {
        #region Свойства
        #region Идентификатор связи работника и задачи.
        /// <summary>
        /// Идентификатор связи работника и задачи.
        /// </summary>
        [Key, Column("ID"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region ID работника.
        /// <summary>
        /// ID работника.
        /// </summary>
        [Column("ID_Worker"), Required, ForeignKey(nameof(Worker))]
        public int IdWorker { get; set; }
        #endregion

        #region ID задачи.
        /// <summary>
        /// ID задачи.
        /// </summary>
        [Column("ID_Task"), Required, ForeignKey(nameof(Task))]
        public int IdTask { get; set; }
        #endregion

        #region Является ли автором.
        /// <summary>
        /// Является ли автором.
        /// </summary>
        [Column("IsAuthor"), Required]
        public bool IsAuthor { get; set; }
        #endregion
        #endregion
    }
}
