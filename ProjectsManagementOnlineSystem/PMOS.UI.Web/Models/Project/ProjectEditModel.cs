using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMOS.UI.Web.Models.Project
{
    /// <summary>
    /// Модель представляющая проект для редактирования.
    /// </summary>
    public class ProjectEditModel
    {
        #region Идентификатор проекта.
        /// <summary>
        /// Идентификатор проекта.
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Наименование проекта.
        /// <summary>
        /// Наименование проекта.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(255, ErrorMessage = "Поле {0} должно быть не более {1} символов.")]
        [Display(Name = "Наименование проекта")]
        public string Name { get; set; }
        #endregion

        #region Название компании-заказчика.
        /// <summary>
        /// Название компании-заказчика.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(255, ErrorMessage = "Поле {0} должно быть не более {1} символов.")]
        [Display(Name = "Название компании-заказчика")]
        public string CustomerCompanyName { get; set; }
        #endregion

        #region Название компании-исполнителя.
        /// <summary>
        /// Название компании-исполнителя.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [StringLength(255, ErrorMessage = "Поле {0} должно быть не более {1} символов.")]
        [Display(Name = "Название компании-исполнителя")]
        public string PerformerCompanyName { get; set; }
        #endregion

        #region Дата начала проекта.
        /// <summary>
        /// Дата начала проекта.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [Display(Name = "Дата начала проекта")]
        public DateTime StartDate { get; set; }
        #endregion

        #region Дата окончания проекта.
        /// <summary>
        /// Дата окончания проекта.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [Display(Name = "Дата окончания проекта")]
        public DateTime EndDate { get; set; }
        #endregion

        #region Приоритет проекта.
        /// <summary>
        /// Приоритет проекта.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} является обязательным.")]
        [Display(Name = "Приоритет проекта")]
        public int Priority { get; set; }
        #endregion
    }
}
