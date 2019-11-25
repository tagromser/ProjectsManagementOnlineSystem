using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PMOS.Identity.Stores
{
    /// <summary>
    /// Реализация интерфейса определяющего методы, которые вы применяете, для использования штампа безопасности для указания того,
    /// изменилась ли информация учетной записи пользователя.
    /// Этот штамп обновляется, когда пользователь меняет пароль или добавляет или удаляет логины.
    /// Он содержит методы получения и установки штампа безопасности.
    /// </summary>
    public partial class UserStore<TUser> : IUserSecurityStampStore<TUser>
    {
        #region Устанавливает предоставленную безопасность <paramref name="stamp" /> для указанного <paramref name="user" />.
        /// <summary>
        /// Устанавливает предоставленную безопасность <paramref name="stamp" /> для указанного <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь, которому должна быть установлена штамп безопасности.</param>
        /// <param name="stamp">Установливаемый штамп безопасности.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.</returns>
        public async Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp ?? throw new ArgumentNullException(nameof(stamp));

            await Task.CompletedTask;
        }
        #endregion

        #region Получает штамп безопасности для указанного <paramref name="user" />.
        /// <summary>
        /// Получает штамп безопасности для указанного <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь, для которого должен быть установлен штамп безопасности.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" />  используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую штама безопасности для указанного <paramref name="user" />.</returns>
        public async Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.FromResult(user.SecurityStamp);
        }
        #endregion
    }
}
