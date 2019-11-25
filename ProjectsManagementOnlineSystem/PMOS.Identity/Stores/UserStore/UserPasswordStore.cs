using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PMOS.Identity.Stores
{
    /// <summary>
    /// Реализация интерфейса определяющего методы, которые можно реализовать для сохранения Хешированные пароли.
    /// Содержит методы для получения и задания хэшированный пароль и метод, который указывает установил ли пользователь пароль.
    /// </summary>
    public partial class UserStore<TUser> : IUserPasswordStore<TUser>
    {
        #region Устанавливает хэш пароля для указанного <paramref name="user" />.
        /// <summary>
        /// Устанавливает хэш пароля для указанного <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь, для которого задается хэш пароля.</param>
        /// <param name="passwordHash">Задаваемый хэш пароля.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию</returns>
        public async Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            await Task.CompletedTask;
        }
        #endregion

        #region Получает хэш пароля для указанного <paramref name="user" />.
        /// <summary>
        /// Получает хэш пароля для указанного <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь у которого необходимо получить хэш пароля.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, возвращая хэш пароля для указанного <paramref name="user" />.</returns>
        public async Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.FromResult(user.PasswordHash);
        }
        #endregion

        #region Получает флаг, указывающий, имеет ли указанный <paramref name="user" /> пароль.
        /// <summary>
        /// Получает флаг, указывающий, имеет ли указанный <paramref name="user" /> пароль.
        /// </summary>
        /// <param name="user">Пользователь у которого нужно вернуть флаг, указывающий есть ли у ниего пароль или нет.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns>
        /// <see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, возвращую true, если указанный <paramref name="user" /> имеет пароль, иначе false.
        /// </returns>
        public async Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(user.PasswordHash != null);
        }
        #endregion
    }
}
