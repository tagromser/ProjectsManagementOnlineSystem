using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMOS.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс общего репозитория для работы с сущностями.
    /// </summary>
    /// <typeparam name="TEntity">Сущность базы.</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        Task<TEntity> FindById(int id);
        Task Create(TEntity item);
        Task Update(TEntity item);
        Task Delete(TEntity item);
        Task DeleteRange(IEnumerable<TEntity> items);
    }
}