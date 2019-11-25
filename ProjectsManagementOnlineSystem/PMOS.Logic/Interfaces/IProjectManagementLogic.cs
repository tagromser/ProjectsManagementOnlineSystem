using PMOS.Logic.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMOS.Logic.Interfaces
{
    /// <summary>
    /// Интерфейс логики управления проектом.
    /// </summary>
    public interface IProjectManagementLogic
    {
        Task<List<ProjectDTO>> GetProject();
    }
}
