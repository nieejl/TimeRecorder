using System.Collections.Generic;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.RepositoryInterfaces
{
    public interface IProjectRepository : ICrudRepository<ProjectDTO>
    {
        Task<IEnumerable<ProjectDTO>> Read();
    }
}
