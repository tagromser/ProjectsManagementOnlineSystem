using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMOS.DataAccess.Model.PMOS.Tables;
using Task = System.Threading.Tasks.Task;

namespace PMOS.Identity.Stores
{
    /// <summary>
    /// Реализация интерфейса определяющего методы, которые можно реализовать,
    /// чтобы сопоставить пользователя с ролью.
    /// Содержит методы для добавления, удаления и извлечения роли пользователя,
    /// а также метод для проверки, если пользователь назначен роли.
    /// </summary>
    public partial class UserStore<TUser> : IUserRoleStore<TUser>
    {
        #region Добавляет указанного <paramref name="user" /> к указанной роли.
        /// <summary>
        /// Добавляет указанного <paramref name="user" /> к указанной роли.
        /// </summary>
        /// <param name="user">Пользователь который добавляется к указанной роли.</param>
        /// <param name="roleName">Имя роли к которой должен быть добавлен пользователь.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию.</returns>
        public async Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException($"Значение {nameof(roleName)} не может содержать null или быть пустым ");

            var role = _pmosContext.Roles.FirstOrDefaultAsync(item => item.Name == roleName);

            if (role == null)
                throw new InvalidOperationException($"Роль {nameof(roleName)} не найдена.");

            _pmosContext.UserRoles.Add(new UserRole { IdUser = user.Id, IdRole = role.Id });

            await _pmosContext.SaveChangesAsync(cancellationToken);

            await Task.CompletedTask;
        }
        #endregion

        #region Удаляет указанного <paramref name="user" /> из указанной роли.
        /// <summary>
        /// Удаляет указанного <paramref name="user" /> из указанной роли.
        /// </summary>
        /// <param name="user">Пользователь который удаляется из указанной роли.</param>
        /// <param name="roleName">Имя роли из которой должен быть удален пользователь.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию.</returns>
        public async Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException($"Значение {nameof(roleName)} не может содержать null или быть пустым ");

            var roleEntity = await _pmosContext.Roles.FirstOrDefaultAsync(item => item.Name == roleName, cancellationToken);

            if (roleEntity != null)
            {
                var userRole = await _pmosContext.UserRoles.FirstOrDefaultAsync(item => item.IdUser == user.Id &&
                                                                                       item.IdRole == roleEntity.Id,
                    cancellationToken);

                if (userRole != null)
                {
                    _pmosContext.UserRoles.Remove(userRole);
                }
            }
        }
        #endregion

        #region Возвращет список имен ролей к которым принадлежит указанный <paramref name="user" />.
        /// <summary>
        /// Возвращет список имен ролей к которым принадлежит указанный <paramref name="user" />.
        /// </summary>
        /// <param name="user">Пользователь чьи имена ролей нужно получить.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns><see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую список имен ролей.</returns>
        public async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var query = from userRole in _pmosContext.UserRoles
                        join role in _pmosContext.Roles on userRole.IdRole equals role.Id
                        where userRole.IdUser.Equals(user.Id)
                        select role.Name;

            return await query.ToListAsync(cancellationToken);
        }
        #endregion

        #region Возвращает флаг указывающий на то относится ли <paramref name="user" /> к указанному имени роли.
        /// <summary>
        /// Возвращает флаг указывающий на то относится ли <paramref name="user" /> к указанному имени роли.
        /// </summary>
        /// <param name="user">Пользователь, у которого нудно проверить относится ли он к указанной роли или нет.</param>
        /// <param name="roleName">Имя роли которая будет проверяться.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns>
        /// <see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую флаг указывающий на то относится ли <paramref name="user" /> к указанной роли.
        /// </returns>
        public async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException($"Значение {nameof(roleName)} не может содержать null или быть пустым ");

            var role = await _pmosContext.Roles.FirstOrDefaultAsync(item => item.Name == roleName, cancellationToken);

            if (role == null)
                return false;

            var userRole = await _pmosContext.UserRoles.FirstOrDefaultAsync(item => item.IdUser == user.Id &&
                                                                                   item.IdRole == role.Id, cancellationToken);

            return userRole != null;
        }
        #endregion

        #region Возвращает список пользователей которые относятся к указанному имени роли.
        /// <summary>
        /// Возвращает список пользователей которые относятся к указанному имени роли.
        /// </summary>
        /// <param name="roleName">Имя роли пользователи которой должны быть возвращены.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /> используется для распространения уведомлений о том, что операция должна быть отменена.</param>
        /// <returns>
        /// <see cref="T:System.Threading.Tasks.Task" /> представляет асинхронную операцию, содержащую список пользователей которые относятся к указанному имени роли.
        /// </returns>
        public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ThrowIfDisposed();

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException(nameof(roleName));

            var role = await _pmosContext.Roles.FirstOrDefaultAsync(item => item.Name == roleName, cancellationToken);

            if (role == null)
                return new List<TUser>();

            var query = from userRole in _pmosContext.UserRoles
                        join user in _pmosContext.Users on userRole.IdUser equals user.Id
                        where userRole.IdRole.Equals(role.Id)
                        select user;

            var usersDb = await query.ToListAsync(cancellationToken);

            IList<TUser> users = new List<TUser>();

            foreach (User user in usersDb)
            {
                users.Add(ConvertUserToGenericUser(user));
            }

            return users;
        }
        #endregion
    }
}