using System;
using PMOS.DTO.Account;

namespace PMOS.Identity.Stores
{
    public partial class RoleStore<TRole> : IDisposable where TRole : RoleDTO, new()
    {
        #region Локальные переменные
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
    }
}
