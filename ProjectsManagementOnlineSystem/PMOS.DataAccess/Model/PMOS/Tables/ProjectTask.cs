using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMOS.DataAccess.Model.PMOS.Tables
{
    /// <summary>
    /// Задача проекта.
    /// </summary>
    [Table("ProjectTask", Schema = "PMOS")]
    public class ProjectTask
    {
        #region Свойства
        #region Идентификатор связи проекта и задачи.
        /// <summary>
        /// Идентификатор связи проекта и задачи.
        /// </summary>
        [Key, Column("ID"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region ID проекта.
        /// <summary>
        /// ID проекта.
        /// </summary>
        [Column("ID_Project"), Required, ForeignKey(nameof(Project))]
        public int IdProject { get; set; }
        #endregion

        #region ID задачи.
        /// <summary>
        /// ID задачи.
        /// </summary>
        [Column("ID_Task"), Required, ForeignKey(nameof(Task))]
        public int IdTask { get; set; }
        #endregion
        #endregion
    }
}
