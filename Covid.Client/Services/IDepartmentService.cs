using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Shared.Dtos;

namespace Covid.Client.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
    }
}