using System.Collections.Generic;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.LocalStorage
{
    public interface ITagRepository : ICrudRepository<TagDTO>
    {
        Task<IEnumerable<TagDTO>> FindByNameAsync(string name);
    }
}
