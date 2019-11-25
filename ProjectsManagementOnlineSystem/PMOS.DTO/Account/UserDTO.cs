using System;
using System.Collections.Generic;
using System.Text;

namespace PMOS.DTO.Account
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class UserDTO
    {
        #region Свойства
        #region Идентификатор пользователя.
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Имя/ник пользователя.
        /// <summary>
        /// Имя/ник пользователя.
        /// </summary>
        public string UserName { get; set; }
        #endregion

        #region Хэш пароля.
        /// <summary>
        /// Хэш пароля.
        /// </summary>
        public string PasswordHash { get; set; }
        #endregion

        #region Некоторое значение, которое меняется при каждой смене настроек аутентификации для данного пользователя.
        /// <summary>
        /// Некоторое значение, которое меняется при каждой смене настроек аутентификации для данного пользователя.
        /// </summary>
        public string SecurityStamp { get; set; }
        #endregion

        //#region Email пользователя.
        ///// <summary>
        ///// Email пользователя.
        ///// </summary>
        //public string Email { get; set; }
        //#endregion

        //#region Email был подтвержден или нет.
        ///// <summary>
        ///// Email был подтвержден или нет.
        ///// </summary>
        //public bool EmailConfirmed { get; set; }
        //#endregion

        //#region Акаунт включен или выключен (Заблокированный пользователь или нет).
        ///// <summary>
        ///// Акаунт включен или выключен (Заблокированный пользователь или нет).
        ///// </summary>
        //public bool LockoutEnabled { get; set; }
        //#endregion

        //#region DateTime в UTC, когда заканчивается блокировка, если время указано до текущего, то считается что нет блокировки.
        ///// <summary>
        ///// DateTime в UTC, когда заканчивается блокировка, если время указано до текущего, то считается что нет блокировки.
        ///// </summary>
        //public DateTimeOffset? LockoutEnd { get; set; }
        //#endregion

        //#region Число попыток неудачного входа в систему.
        ///// <summary>
        ///// Число попыток неудачного входа в систему.
        ///// </summary>
        //public int AccessFailedCount { get; set; }
        //#endregion
        #endregion
    }
}
