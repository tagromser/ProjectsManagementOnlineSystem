using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMOS.UI.Web.Models.Product
{
    /// <summary>
    /// Проект.
    /// </summary>
    public class ProjectModel
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
    }
}
