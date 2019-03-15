using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.LocalStorage
{
    public interface ITagRepository : ICrudRepository<TagDTO>
    {
        Task<IEnumerable<TagDTO>> FindByNameAsync(string name);
    }
}
