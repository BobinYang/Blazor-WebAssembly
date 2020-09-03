using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Server.Entities;

namespace Covid.Server.Services
{
    public interface IDailyHealthRepository
    {
        Task UpdateForDepartmentAsync(int departmentId, DateTime date, IList<DailyHealth> dailyHealths);
        Task<IList<DailyHealth>> GetByDepartmentAsync(int departmentId, DateTime date);
    }
}