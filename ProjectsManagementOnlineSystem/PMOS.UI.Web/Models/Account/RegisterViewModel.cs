using System.ComponentModel.DataAnnotations;

namespace PMOS.UI.Web.Models.Account
{
    /// <summary>
    /// Модель страницы регистрации.
    /// </summary>
    public class RegisterViewModel
    {
        #region Логин.
        /// <summary>
        /// Логин.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(100, ErrorMessage = "Поле {0} должно быть не менее {2} символов и максимальная длина должна составлять {1} символов.", MinimumLength = 6)]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        #endregion

        #region Пароль.
        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(100, ErrorMessage = "Поле {0} должно быть не менее {2} символов и максимальная длина должна составлять {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        #endregion

        #region Подтверждение пароля.
        /// <summary>
        /// Подтверждение пароля.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare(nameof(Password), ErrorMessage = "Поля {0} и {1} не совпадают.")]
        public string ConfirmPassword { get; set; }
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

        #region ID роли.
        /// <summary>
        /// ID роли.
        /// </summary>
        public int IdRole => 1;
        #endregion
    }
}
