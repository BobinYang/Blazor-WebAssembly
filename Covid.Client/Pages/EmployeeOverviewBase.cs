using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Covid.Client.Services;
using Covid.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Covid.Client.Pages
{
    public class EmployeeOverviewBase: ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }

        public IEnumerable<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

        public string Result { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeService.GetForDepartmentAsync(1);
            await base.OnInitializedAsync();
        }

        public async Task SayHello()
        {
            Result = await JsRuntime.InvokeAsync<string>("sayHello", "Mike");
        }
    }

}
