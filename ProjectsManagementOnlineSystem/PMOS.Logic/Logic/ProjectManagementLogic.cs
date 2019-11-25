using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMOS.DataAccess.Model.PMOS.Tables;
using PMOS.DataAccess.Context;
using PMOS.Logic.DTO;
using PMOS.Logic.Interfaces;
using System.Linq;

namespace PMOS.Logic.Logic
{
    /// <summary>
    /// Реализация интерфейса логики управления проектов.
    /// </summary>
    public class ProjectManagementLogic : IProjectManagementLogic
    {
        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        public ProjectManagementLogic(PMOSContext pmosContext)
        {
            _pmosContext = pmosContext;
        }
        #endregion

        #region Локальные переменные
        #region Контекст базы данных.
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private PMOSContext _pmosContext;
        #endregion
        #endregion

        public async Task<List<ProjectDTO>> GetProject()
        {
            List<Project> projects = _pmosContext.Projects.Include("Project").ToList();

            List<ProjectDTO> ProjectsDTO = new List<ProjectDTO>();

            foreach (var project in projects)
            {
                ProjectsDTO.Add(new ProjectDTO()
                {
                    Id = project.Id,
                    Name = project.Name
                });
            }

            return ProjectsDTO;
        }

    }
}
