using System;
using PMOS.DataAccess.Context;
using PMOS.DataAccess.Model.PMOS.Tables;
using PMOS.DTO.Account;

namespace PMOS.Identity.Stores
{
    /// <summary>
    /// Хранилище пользователей.
    /// </summary>
    /// <typeparam name="TUser">Пользователь.</typeparam>
    public partial class UserStore<TUser> : IDisposable where TUser : UserDTO, new()
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pmosContext">Контекст базы данных".</param>
        public UserStore(PMOSContext pmosContext)
        {
            _pmosContext = pmosContext;
        }
        #endregion

        #region Локальные переменные
        #region Контекст базы данных.
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private PMOSContext _pmosContext;
        #endregion

        #region Если true будет вызван dispose над репозиторием данных во время dispose.
        /// <summary>
        /// Если true будет вызван dispose над репозиторием данных во время dispose.
        /// </summary>
        private bool _disposed = false;
        #endregion
        #endregion

        #region public методы
        #region Реализация IDisposable
        #region Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            //Подавляем финализацию.
            GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
        #endregion

        #region protected методы
        #region Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        /// <param name="disposing">Освободить ресурсы?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                //Освобождаем управляемые ресурсы.
            }

            //Освобождаем неуправляемые объекты.
            _disposed = true;
        }
        #endregion
        #endregion

        #region private методы
        #region Проверка на то dispose или нет, если dispose выдает сообщение об ошибке.
        /// <summary>
        /// Проверка на то dispose или нет, если dispose выдает сообщение об ошибке.
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
        #endregion

        #region Конвертация из generic пользователя в пользователя.
        /// <summary>
        /// Конвертация из generic пользователя в пользователя.
        /// </summary>
        /// <param name="user">Generic пользователь.</param>
        /// <returns>Пользователь.</returns>
        private User ConvertGenericUserToUser(TUser user)
        {
            return new User
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp
                //Email = user.Email,
                //EmailConfirmed = user.EmailConfirmed,
                //LockoutEnabled = user.LockoutEnabled,
                //LockoutEnd = user.LockoutEnd,
                //AccessFailedCount = user.AccessFailedCount
            };
        }
        #endregion

        #region Конвертация из пользователя в generic пользователя.
        /// <summary>
        /// Конвертация из пользователя в generic пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Generic пользователь.</returns>
        private TUser ConvertUserToGenericUser(User user)
        {
            return new TUser
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp
                //Email = user.Email,
                //EmailConfirmed = user.EmailConfirmed,
                //LockoutEnabled = user.LockoutEnabled,
                //LockoutEnd = user.LockoutEnd,
                //AccessFailedCount = user.AccessFailedCount
            };
        }
        #endregion
        #endregion
    }
}
