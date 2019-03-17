using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services
{
    public interface ICrudRepository<T> where T : LocalEntity
    {
        Task<int> CreateAsync(T dto);
        Task<T> FindAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(T dto);
    }
}
