using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Server.Data;
using Covid.Server.Entities;
using Covid.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Covid.Server.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyDbContext _myContext;

        public EmployeeRepository(MyDbContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<IList<Employee>> GetForDepartmentAsync(int departmentId)
        {
            var employees = await _myContext.Employees
                .Where(x => x.DepartmentId == departmentId).ToListAsync();
            return employees;
        }

        public async Task<Employee> GetOneAsync(int departmentId, int id)
        {
            var one = await _myContext.Employees
                .SingleOrDefaultAsync(x => x.DepartmentId == departmentId && x.Id == id);
            return one;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _myContext.Employees.AddAsync(employee);
            var count = await _myContext.SaveChangesAsync();
            if (count != 1)
            {
                throw new Exception("新增失败");
            }

            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            _myContext.Attach(employee);
            _myContext.Employees.Update(employee);
            var count = await _myContext.SaveChangesAsync();
            if (count != 1)
            {
                throw new Exception("修改失败");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var exist = await _myContext.Employees.FindAsync(id);
            if (exist == null)
            {
                throw new Exception("未能找到该员工");
            }
            _myContext.Employees.Remove(exist);
            var count = await _myContext.SaveChangesAsync();
            if (count != 1)
            {
                throw new Exception("删除失败");
            }
        }
    }
}
