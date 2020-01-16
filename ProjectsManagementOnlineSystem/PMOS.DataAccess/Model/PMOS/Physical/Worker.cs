using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMOS.DataAccess.Model.PMOS.Physical
{
    /// <summary>
    /// Работник.
    /// </summary>
    [Table("Worker", Schema = "PMOS")]
    public class Worker
    {
        #region Свойства
        #region Идентификатор работника.
        /// <summary>
        /// Идентификатор работника.
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

        #region Имя работника.
        /// <summary>
        /// Имя работника.
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }
        #endregion

        #region Фамилия работника.
        /// <summary>
        /// Фамилия работника.
        /// </summary>
        [Column("Surname"), Required]
        public string Surname { get; set; }
        #endregion

        #region Отчество работника.
        /// <summary>
        /// Отчество работника.
        /// </summary>
        [Column("Patronymic"), Required]
        public string Patronymic { get; set; }
        #endregion

        #region Email работника.
        /// <summary>
        /// Email работника.
        /// </summary>
        [Column("Email")]
        public string Email { get; set; }
        #endregion
        #endregion
    }
}
