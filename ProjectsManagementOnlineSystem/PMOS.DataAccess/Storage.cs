using Microsoft.EntityFrameworkCore.Storage;
using PMOS.DataAccess.Context;
using PMOS.DataAccess.Interfaces;
using PMOS.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace PMOS.DataAccess
{
    /// <summary>
    /// Реализует интерфейс хранилища объединяющий в себе репозитории для работы с данными.
    /// </summary>
    public class Storage : IStorage
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pmosContext">Контекст для работы с базой данных.</param>
        public Storage(PMOSContext pmosContext)
        {
            this.pmosContext = pmosContext;
        }
        #endregion

        #region Локальные переменные
        /// <summary>
        /// Контекст для работы с базой данных.
        /// </summary>
        private readonly PMOSContext pmosContext;

        /// <summary>
        /// Словарь для хранения связки названия репозитория и его экземпляра.
        /// </summary>
        private Dictionary<string, object> repositories;

        /// <summary>
        /// Виртуальный репозиторий для работы с несколькими сущностями или со сложными запросами к сущностям.
        /// </summary>
        private VirtualRepository virtualRepository;
        #endregion


        /// <summary>
        /// Получение неоходимого репозитория для работы с сущностями.
        /// </summary>
        /// <typeparam name="TEntity">Сущность базы.</typeparam>
        /// <returns>Репозиторий для работы с сущностями.</returns>
        public GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), pmosContext);
                repositories.Add(type, repositoryInstance);
            }

            return (GenericRepository<TEntity>)repositories[type];
        }

        /// <summary>
        /// Репозиторий для работы с несколькими сущностями.
        /// </summary>
        public VirtualRepository VirtualRepository
        {
            get
            {
                if (virtualRepository == null)
                    virtualRepository = new VirtualRepository(pmosContext);
                return virtualRepository;
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return pmosContext.Database.BeginTransaction();
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    pmosContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}
