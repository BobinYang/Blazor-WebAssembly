using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Shared.Dtos;

namespace Covid.Client.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetForDepartmentAsync(int departmentId);
        Task<EmployeeDto> GetOneForDepartmentAsync(int departmentId, int id);
        Task<EmployeeDto> AddForDepartmentAsync
            (int departmentId, EmployeeAddOrUpdateDto employee);
        Task UpdateForDepartmentAsync
            (int departmentId, int id, EmployeeAddOrUpdateDto employee);
        Task DeleteFromDepartmentAsync(int departmentId, int id);
    }
}