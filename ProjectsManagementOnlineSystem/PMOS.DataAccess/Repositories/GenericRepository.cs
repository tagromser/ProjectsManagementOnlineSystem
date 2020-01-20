using Microsoft.EntityFrameworkCore;
using PMOS.DataAccess.Context;
using PMOS.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMOS.DataAccess.Repositories
{
    /// <summary>
    /// Реализация интерфейса общего репозитория для работы с сущностями.
    /// </summary>
    /// <typeparam name="TEntity">Сущность базы.</typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pmosContext">Контекст для работы с базой данных.</param>
        public GenericRepository(PMOSContext pmosContext)
        {
            this.pmosContext = pmosContext;
            dbSet = pmosContext.Set<TEntity>();
        }
        #endregion

        #region Локальные переменные
        /// <summary>
        /// Контекст для работы с базой данных.
        /// </summary>
        private readonly PMOSContext pmosContext;

        /// <summary>
        /// Используется для запроса и сохранения экземпляров сущности.
        /// </summary>
        private readonly DbSet<TEntity> dbSet;
        #endregion

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return dbSet.Where(predicate);
        }

        public async Task<TEntity> FindById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Create(TEntity item)
        {
            await dbSet.AddAsync(item);
            await pmosContext.SaveChangesAsync();
        }

        public async Task Update(TEntity item)
        {
            pmosContext.Entry(item).State = EntityState.Modified;
            await pmosContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity item)
        {
            dbSet.Remove(item);
            await pmosContext.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<TEntity> items)
        {
            dbSet.RemoveRange(items);
            await pmosContext.SaveChangesAsync();
        }
    }
}
