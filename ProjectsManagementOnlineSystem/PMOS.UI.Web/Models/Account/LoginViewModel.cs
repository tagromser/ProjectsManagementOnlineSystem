using System.ComponentModel.DataAnnotations;

namespace PMOS.UI.Web.Models.Account
{
    /// <summary>
    /// Модель страницы авторизации.
    /// </summary>
    public class LoginViewModel
    {
        #region Имя/ник.
        /// <summary>
        /// Имя/ник.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(100, ErrorMessage = "Поле {0} должно быть не менее {2} символов и максимальная длина должна составлять {1} символов.", MinimumLength = 6)]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        #endregion

        #region Пароль.
        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        #endregion
    }
}
