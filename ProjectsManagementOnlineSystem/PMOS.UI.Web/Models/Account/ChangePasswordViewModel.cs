using System.ComponentModel.DataAnnotations;

namespace PMOS.UI.Web.Models.Account
{
    public class ChangePasswordViewModel
    {
        #region Пароль.
        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(100, ErrorMessage = "Поле {0} должно быть не менее {2} символов и максимальная длина должна составлять {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string Password { get; set; }
        #endregion

        #region Подтверждение пароля.
        /// <summary>
        /// Подтверждение пароля.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение нового пароля")]
        [Compare(nameof(Password), ErrorMessage = "Поля {0} и {1} не совпадают.")]
        public string ConfirmPassword { get; set; }
        #endregion
    }
}
