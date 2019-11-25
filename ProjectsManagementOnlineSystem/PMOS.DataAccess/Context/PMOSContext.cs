using Microsoft.EntityFrameworkCore;

namespace PMOS.DataAccess.Context
{
    /// <summary>
    /// Контекст для работы с базой данных.
    /// </summary>
    public sealed partial class PMOSContext: DbContext
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        public PMOSContext(DbContextOptions<PMOSContext> options) : base(options)
        { }
        #endregion
    }
}
