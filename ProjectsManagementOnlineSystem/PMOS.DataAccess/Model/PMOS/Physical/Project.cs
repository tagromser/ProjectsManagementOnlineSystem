using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMOS.DataAccess.Model.PMOS.Physical
{
    /// <summary>
    /// Проект.
    /// </summary>
    [Table("Project", Schema = "PMOS")]
    public class Project
    {
        #region Свойства
        #region Идентификатор проекта.
        /// <summary>
        /// Идентификатор проекта.
        /// </summary>
        [Key, Column("ID"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region Название проекта.
        /// <summary>
        /// Название проекта.
        /// </summary>
        [Column("Name"), Required]
        public string Name { get; set; }
        #endregion

        #region Название компании-заказчика.
        /// <summary>
        /// Название компании-заказчика.
        /// </summary>
        [Column("CustomerCompanyName"), Required]
        public string CustomerCompanyName { get; set; }
        #endregion

        #region Название компании-исполнителя.
        /// <summary>
        /// Название компании-исполнителя.
        /// </summary>
        [Column("PerformerCompanyName"), Required]
        public string PerformerCompanyName { get; set; }
        #endregion

        #region Дата начала проекта.
        /// <summary>
        /// Дата начала проекта.
        /// </summary>
        [Column("StartDate"), Required]
        public DateTime StartDate { get; set; }
        #endregion

        #region Дата окончания проекта.
        /// <summary>
        /// Дата окончания проекта.
        /// </summary>
        [Column("EndDate"), Required]
        public DateTime EndDate { get; set; }
        #endregion

        #region Приоритет проекта.
        /// <summary>
        /// Приоритет проекта.
        /// </summary>
        [Column("Priority"), Required]
        public int Priority { get; set; }
        #endregion
        #endregion
    }
}
