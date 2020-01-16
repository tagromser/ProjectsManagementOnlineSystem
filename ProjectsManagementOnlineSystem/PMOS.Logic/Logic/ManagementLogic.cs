using AutoMapper;
using PMOS.DataAccess.Interfaces;

namespace CCFI.Logic.Logics
{
    /// <summary>
    /// Базовый класс логики управления.
    /// </summary>
    public class ManagementLogic
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pmosContext">Контекст для работы с базой данных PMOS</param>
        /// <param name="mapper">Маппер для маппинга объектов.</param>
        public ManagementLogic(IStorage storage, IMapper mapper)
        {
            this.storage = storage;
            this.mapper = mapper;
        }
        #endregion

        #region Локальные переменные
        #region Контекст базы данных: "ClosedClubForInvestors".
        /// <summary>
        /// Контекст базы данных: "StoreAccountingSystem".
        /// </summary>
        protected readonly IStorage storage;
        #endregion

        #region Маппер для маппинга объектов.
        /// <summary>
        /// Маппер для маппинга объектов.
        /// </summary>
        protected readonly IMapper mapper;
        #endregion
        #endregion
    }
}
