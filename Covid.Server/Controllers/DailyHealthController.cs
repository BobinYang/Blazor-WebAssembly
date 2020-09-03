using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Server.Entities;
using Covid.Server.Services;
using Covid.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Covid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/{departmentId}/{date}")]
    public class DailyHealthController: ControllerBase
    {
        private readonly IDailyHealthRepository _dailyHealthRepository;

        public DailyHealthController(IDailyHealthRepository dailyHealthRepository)
        {
            _dailyHealthRepository = dailyHealthRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<DailyHealthDto>>> Get
            (int departmentId, DateTime date)
        {
            var entities = await _dailyHealthRepository
                .GetByDepartmentAsync(departmentId, date);
            var dtos = entities.Select(x => new DailyHealthDto
            {
                Date = x.Date,
                EmployeeId = x.EmployeeId,
                HealthCondition = x.HealthCondition,
                Remark = x.Remark,
                Temperature = x.Temperature
            }).ToList();
            return dtos;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<DailyHealthDto>>> Get
            (int departmentId, DateTime date, List<DailyHealthDto> dailyHealths)
        {
            var entities = dailyHealths.Select(x=> new DailyHealth
            {
                Date = x.Date,
                EmployeeId = x.EmployeeId,
                HealthCondition = x.HealthCondition,
                Remark = x.Remark,
                Temperature = x.Temperature
            }).ToList();
            await _dailyHealthRepository.UpdateForDepartmentAsync(departmentId, date, entities);

            var updatedEntities = await _dailyHealthRepository
                .GetByDepartmentAsync(departmentId, date);
            var dtos = updatedEntities
                .Select(x => new DailyHealthDto
            {
                Date = x.Date,
                EmployeeId = x.EmployeeId,
                HealthCondition = x.HealthCondition,
                Remark = x.Remark,
                Temperature = x.Temperature
            }).ToList();
            return dtos;
        }
    }
}
