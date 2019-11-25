using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMOS.DataAccess.Model.PMOS.Tables
{
    /// <summary>
    /// Работник проекта.
    /// </summary>
    [Table("ProjectWorker", Schema = "PMOS")]
    public class ProjectWorker
    {
        #region Свойства
        #region Идентификатор связи проекта и работника.
        /// <summary>
        /// Идентификатор связи проекта и работника.
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

        #region ID работника.
        /// <summary>
        /// ID работника.
        /// </summary>
        [Column("ID_Worker"), Required, ForeignKey(nameof(Worker))]
        public int IdWorker { get; set; }
        #endregion
        #endregion
    }
}
