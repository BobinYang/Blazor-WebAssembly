using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Server.Entities;

namespace Covid.Server.Services
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int departmentId);
    }
}
