using System.ComponentModel.DataAnnotations;

namespace PMOS.UI.Web.Models.Worker
{
    /// <summary>
    /// Модель представляющая работника для редактирования.
    /// </summary>
    public class WorkerEditModel
    {
        #region Идентификатор.
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Идентификатор пользователя.
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int IdUser { get; set; }
        #endregion

        #region Имя.
        /// <summary>
        /// Имя.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(50, ErrorMessage = "Максимальная длина поля {0} должна составлять {1} символов.")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        #endregion

        #region Фамилия.
        /// <summary>
        /// Фамилия.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(50, ErrorMessage = "Максимальная длина поля {0} должна составлять {1} символов.")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        #endregion

        #region Отчество.
        /// <summary>
        /// Отчество.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(50, ErrorMessage = "Максимальная длина поля {0} должна составлять {1} символов.")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        #endregion

        #region Почта.
        /// <summary>
        /// Почта.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(100, ErrorMessage = "Поле {0} должно быть не менее {2} символов и максимальная длина должна составлять {1} символов.", MinimumLength = 6)]
        [RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}$", ErrorMessage = "Недопустимый адрес электронной почты")]
        [Display(Name = "Почта")]
        public string Email { get; set; }
        #endregion
    }
}
