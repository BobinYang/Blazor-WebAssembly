using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Server.Data;
using Covid.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Covid.Server.Services
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly MyDbContext _context;

        public DepartmentRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            var all = await _context.Departments.ToListAsync();
            return all;
        }

        public async Task<Department> GetByIdAsync(int departmentId)
        {
            var one = await _context.FindAsync<Department>(departmentId);
            return one;
        }
    }
}
