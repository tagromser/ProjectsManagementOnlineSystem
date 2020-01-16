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
        private readonly PMOSContext pmosContext;
        private Dictionary<string, object> repositories;
        private VirtualRepository virtualRepository;

        public Storage(PMOSContext pmosContext)
        {
            this.pmosContext = pmosContext;
        }

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
