using System.Threading.Tasks;
using AutoMapper;
using CCFI.UI.Web.Controllers.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMOS.DTO;
using PMOS.DTO.Account;
using PMOS.Identity.Managers;

using PMOS.UI.Web.Models.Account;

namespace PMOS.UI.Web.Controllers
{
    [Authorize]
    public class AccountController : GenericController
    {
        #region Конструктор.
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userManager">Предоставляет API для управления пользователями в хранилище.</param>
        /// <param name="signInManager">Предоставляет API для входа пользователя.</param>
        /// <param name="mapper">Маппер для маппинга объектов.</param>
        public AccountController(UserManager userManager, SignInManager<UserDTO> signInManager, IMapper mapper) : base(mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #endregion

        #region Локальные переменные
        /// <summary>
        /// Предоставляет API для управления пользователями в хранилище.
        /// </summary>
        private readonly UserManager userManager;

        /// <summary>
        /// Предоставляет API для входа пользователя.
        /// </summary>
        private readonly SignInManager<UserDTO> signInManager;
        #endregion

        #region public методы.
        #region Отображает страницу входа в систему. Метод: "GET". Адрес: "/Account/Login".
        /// <summary>
        /// Отображает страницу входа в систему. Метод: "GET". Адрес: "/Account/Login".
        /// </summary>
        /// <param name="returnUrl">Адрес на который будет осуществлен переход после авторизации.</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Очищаем существующий внешний файл cookie, чтобы обеспечить чистый процесс входа в систему.
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        #endregion

        #region Осуществляет вход в систему. Метод "POST". Адрес: "/Account/Login".
        /// <summary>
        /// Осуществляет вход в систему. Метод "POST". Адрес: "/Account/Login".
        /// </summary>
        /// <param name="model">Модель страницы авторизации.</param>
        /// <param name="returnUrl">Адрес на который будет осуществлен переход после авторизации.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var result = await signInManager.PasswordSignInAsync(model.Name, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
                return RedirectToLocal(returnUrl);

            if (result.IsLockedOut)
                return RedirectToAction(nameof(Lockout));

            ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
            return View(model);
        }
        #endregion

        #region Отображает страницу, что учетная запись заблокирована. Метод "GET". Адрес: "/Account/Lockout".
        /// <summary>
        /// Отображает страницу, что учетная запись заблокирована. Метод "GET". Адрес: "/Account/Lockout".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }
        #endregion

        #region Отображает страницу регистрации в системе. Метод: "GET". Адрес: "/Account/Register".
        /// <summary>
        /// Отображает страницу регистрации в системе. Метод: "GET". Адрес: "/Account/Register".
        /// </summary>
        /// <param name="returnUrl">Адрес на который будет осуществлен переход после регистрации.</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }
        #endregion

        /// <summary>
        /// Осуществляет регистрацию в системе. Метод "POST". Адрес: "/Account/Register".
        /// </summary>
        /// <param name="model">Модель страницы авторизации.</param>
        /// <param name="returnUrl">Адрес на который будет осуществлен переход после авторизации.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var user = new UserDTO
            {
                UserName = model.Login
            };

            var workerDTO = new WorkerDTO
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password, workerDTO, model.IdRole);

            if (result.Succeeded)
            {
                if(model.IdRole == 1)
                    await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToLocal(returnUrl);
            }

            AddErrors(result);

            return View(model);
        }

        #region Осуществляет выход из системы. Метод "POST". Адрес: "/Account/Logout".
        /// <summary>
        /// Осуществляет выход из системы. Метод "POST". Адрес: "/Account/Logout".
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion
        #endregion

        #region private методы.
        #region Осуществляет переход на указанный URL.
        /// <summary>
        /// Осуществляет переход на указанный URL.
        /// </summary>
        /// <param name="returnUrl">Адрес на который будет осуществлен переход.</param>
        /// <returns></returns>
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion

        #region Добавляет ошибки, для вывода на странице.
        /// <summary>
        /// Добавляет ошибки, для вывода на странице.
        /// </summary>
        /// <param name="result">Представляет результат операции идентификации.</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
        #endregion
    }
}