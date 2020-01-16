using Microsoft.EntityFrameworkCore.Storage;
using PMOS.DataAccess.Repositories;
using System;

namespace PMOS.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс хранилища объединяющий в себе репозитории для работы с данными.
    /// </summary>
    public interface IStorage : IDisposable
    {
        GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        VirtualRepository VirtualRepository { get; }
        IDbContextTransaction BeginTransaction();
    }
}
