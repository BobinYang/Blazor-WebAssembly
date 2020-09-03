using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Covid.Client.Services;
using Covid.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace Covid.Client.Pages
{
    public class EmployeeDetailBase: ComponentBase
    {
        [Inject] public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public EmployeeDto Employee { get; set; } = new EmployeeDto();

        
        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetOneForDepartmentAsync(1, int.Parse(EmployeeId));
            await base.OnInitializedAsync();
        }

        public void Button_Click()
        {
            Employee.Name = "Roberto Baggio";
        }
    }
}
