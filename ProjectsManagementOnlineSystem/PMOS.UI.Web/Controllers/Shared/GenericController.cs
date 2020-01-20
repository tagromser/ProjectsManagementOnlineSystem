using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CCFI.UI.Web.Controllers.Shared
{
    /// <summary>
    /// Общий контроллер.
    /// </summary>
    public class GenericController : Controller
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="mapper">Маппер для маппинга объектов.</param>
        public GenericController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        #endregion

        #region Локальные переменные
        /// <summary>
        /// Маппер для маппинга объектов.
        /// </summary>
        protected readonly IMapper mapper;
        #endregion

        #region Осуществляет переход по указанной ссылке.
        /// <summary>
        /// Осуществляет переход по указанной ссылке.
        /// </summary>
        /// <param name="returnUrl">Адрес по которому нужно осуществить переход.</param>
        /// <param name="actionName">Название действия.</param>
        /// <returns></returns>
        protected IActionResult RedirectToLocal(string returnUrl, string actionName)
        {
            return RedirectToLocal(returnUrl, actionName, (object)null);
        }
        #endregion

        #region Осуществляет переход по указанной ссылке.
        /// <summary>
        /// Осуществляет переход по указанной ссылке.
        /// </summary>
        /// <param name="returnUrl">Адрес по которому нужно осуществить переход.</param>
        /// <param name="actionName">Название действия.</param>
        /// <param name="routeValues">Параметры для маршрута.</param>
        /// <returns></returns>
        protected IActionResult RedirectToLocal(string returnUrl, string actionName, object routeValues)
        {
            return RedirectToLocal(returnUrl, actionName, (string)null, routeValues);
        }
        #endregion

        #region Осуществляет переход по указанной ссылке.
        /// <summary>
        /// Осуществляет переход по указанной ссылке.
        /// </summary>
        /// <param name="returnUrl">Адрес по которому нужно осуществить переход.</param>
        /// <param name="actionName">Название действия.</param>
        /// <param name="controllerName">Имя контроллера.</param>
        /// <returns></returns>
        protected IActionResult RedirectToLocal(string returnUrl, string actionName, string controllerName)
        {
            return RedirectToLocal(returnUrl, actionName, controllerName, (object)null);
        }
        #endregion

        #region Осуществляет переход по указанной ссылке.
        /// <summary>
        /// Осуществляет переход по указанной ссылке.
        /// </summary>
        /// <param name="returnUrl">Адрес по которому нужно осуществить переход.</param>
        /// <param name="actionName">Название действия.</param>
        /// <param name="controllerName">Имя контроллера.</param>
        /// <param name="routeValues">Параметры для маршрута.</param>
        /// <returns></returns>
        protected IActionResult RedirectToLocal(string returnUrl, string actionName, string controllerName, object routeValues)
        {
            return RedirectToLocal(returnUrl, actionName, controllerName, routeValues, (string)null);
        }
        #endregion

        #region Осуществляет переход по указанной ссылке.
        /// <summary>
        /// Осуществляет переход по указанной ссылке.
        /// </summary>
        /// <param name="returnUrl">Адрес по которому нужно осуществить переход.</param>
        /// <param name="actionName">Название действия.</param>
        /// <param name="controllerName">Имя контроллера.</param>
        /// <param name="fragment">Фрагмент для добавления в URL.</param>
        /// <returns></returns>
        protected IActionResult RedirectToLocal(string returnUrl, string actionName, string controllerName, string fragment)
        {
            return RedirectToLocal(returnUrl, actionName, controllerName, (object)null, fragment);
        }
        #endregion

        #region Осуществляет переход по указанной ссылке.
        /// <summary>
        /// Осуществляет переход по указанной ссылке.
        /// </summary>
        /// <param name="returnUrl">Адрес по которому нужно осуществить переход.</param>
        /// <param name="actionName">Название действия.</param>
        /// <param name="controllerName">Имя контроллера.</param>
        /// <param name="routeValues">Параметры для маршрута.</param>
        /// <param name="fragment">Фрагмент для добавления в URL.</param>
        /// <returns></returns>
        protected IActionResult RedirectToLocal(string returnUrl, string actionName, string controllerName, object routeValues, string fragment)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(actionName, controllerName, routeValues, fragment);
        }
        #endregion
    }
}