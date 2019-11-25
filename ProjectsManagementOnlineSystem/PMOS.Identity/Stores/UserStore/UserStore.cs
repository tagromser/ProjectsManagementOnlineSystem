using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMOS.DataAccess.Model.PMOS.Tables;
using Task = System.Threading.Tasks.Task;

namespace PMOS.Identity.Stores
{
    /// <summary>
    /// Реализация интерфейса определяющего методы для создания, обновления, удаления и получения пользователей.
    /// </summary>
    public partial class UserStore<TUser> : IUserStore<TUser>
    {
        #region Получает идентификатор пользователя для указанного <paramref name="user" />.
        /// <summary>
        /// Получает идентификатор пользователя для указанного <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь, чей идентификатор должен быть извлечен.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую идентификатор для указанного <paramref name="user" />.</returns>
        public async Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.FromResult(user.Id.ToString());
        }
        #endregion

        #region Получает имя пользователя для указанного <paramref name="user" />.
        /// <summary>
        /// Получает имя пользователя для указанного <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь, имя которого должно быть получено.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую имя пользователя для указанного <paramref name="user" />.</returns>
        public async Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.FromResult(user.UserName);
        }
        #endregion

        #region Устанавливает заданное значение <paramref name="userName" /> для указанного <paramref name="user" />.
        /// <summary>
        /// Устанавливает заданное значение <paramref name="userName" /> для указанного <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь, имя которого должно быть установлено.</param>
        /// <param name="userName">Имя пользователя для установки.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию.</returns>
        public async Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UserName = userName;

            await Task.CompletedTask;
        }
        #endregion

        #region Получает нормализованное имя пользователя для указанного <paramref name="user" />.
        /// <summary>
        /// Получает нормализованное имя пользователя для указанного <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь, нормализованное имя которого должно быть получено.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" />, который представляет асинхронную операцию, содержащую нормализованное имя пользователя для указанного <paramref name="user" />.</returns>
        public async Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.FromResult(user.UserName);
        }
        #endregion

        #region Устанавливает заданное нормализованное имя для указанного <paramref name="user" />.
        /// <summary>
        /// Устанавливает заданное нормализованное имя для указанного <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь, имя которого должно быть установлено.</param>
        /// <param name="normalizedName">Нормализованное имя для установки.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> который представляет асинхронную операцию.</returns>
        public async Task SetNormalizedUserNameAsync(TUser user, string normalizedName,
            CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
        #endregion

        #region Создает указанный <paramref name="user" /> в хранилище пользователей.
        /// <summary>
        /// Создает указанный <paramref name="user" /> в хранилище пользователей.
        /// </summary>
        /// <param name="user">Пользователь который должен быть создан.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую операцию создания <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /></returns>
        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _pmosContext.Users.Add(ConvertGenericUserToUser(user));

            await _pmosContext.SaveChangesAsync(cancellationToken);
            
            return IdentityResult.Success;
        }
        #endregion

        #region Обновляет указанный <paramref name="user" /> в хранилище пользователя.
        /// <summary>
        /// Обновляет указанный <paramref name="user" /> в хранилище пользователя.
        /// </summary>
        /// <param name="user">Пользователь для обновления.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую операцию обновления <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />.</returns>
        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userDb = ConvertGenericUserToUser(user);

            _pmosContext.Users.Attach(userDb);
            _pmosContext.Users.Update(userDb);

            try
            {
                await _pmosContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }
        #endregion

        #region Удаляет указанный <paramref name="user" /> из хранилищя пользователя.
        /// <summary>
        /// Удаляет указанный <paramref name="user" /> из хранилищя пользователя.
        /// </summary>
        /// <param name="user">Пользователь котротый должен быть удален.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую операцию обновления <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />.</returns>
        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userDb = ConvertGenericUserToUser(user);

            _pmosContext.Attach(userDb);
            _pmosContext.Users.Remove(userDb);

            try
            {
                await _pmosContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }
        #endregion

        #region Находит и возвращает пользователя, если он есть, с указанным <paramref name="userId" />.
        /// <summary>
        /// Находит и возвращает пользователя, если он есть, с указанным <paramref name="userId" />.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя для поиска.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую пользователя, соответствующего указанному <paramref name="userId" />, если он существует.</returns>
        public async Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            User userDb = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_pmosContext.Users, item => item.Id == Convert.ToInt32(userId),
                    cancellationToken);

            return userDb == null ? null : ConvertUserToGenericUser(userDb);
        }
        #endregion

        #region Находит и возвращает пользователя, если таковой имеется, с указанным нормализованным именем пользователя.
        /// <summary>
        /// Находит и возвращает пользователя, если таковой имеется, с указанным нормализованным именем пользователя.
        /// </summary>
        /// <param name="normalizedUserName">Нормализованное имя пользователя для поиска.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую пользователя, соответствующего указанному <paramref name="normalizedUserName" />, если он существует.</returns>
        public async Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            User userDb = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_pmosContext.Users, item => item.UserName == normalizedUserName, cancellationToken);

            return userDb == null ? null : ConvertUserToGenericUser(userDb);
        }
        #endregion
    }
}
