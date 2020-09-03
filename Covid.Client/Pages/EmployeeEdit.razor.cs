using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Client.Services;
using Covid.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace Covid.Client.Pages
{
    public partial class EmployeeEdit
    {
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Inject] public IDepartmentService DepartmentService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public string DepartmentId { get; set; }
        [Parameter] public string EmployeeId { get; set; }

        public EmployeeAddOrUpdateDto Employee { get; set; } = new EmployeeAddOrUpdateDto();
        public List<DepartmentDto> Departments { get; set; } = new List<DepartmentDto>();

        protected string EmployeeDepartmentId = string.Empty;

        public string Message { get; set; }
        public bool Saved { get; set; }
        public string CssClass { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(EmployeeId))
            {
                var employee = await EmployeeService.GetOneForDepartmentAsync
                    (int.Parse(DepartmentId), int.Parse(EmployeeId));
                Employee = new EmployeeAddOrUpdateDto
                {
                    BirthDate = employee.BirthDate,
                    Gender = employee.Gender,
                    Name = employee.Name,
                    No = employee.No,
                    PictureUrl = employee.PictureUrl
                };
            }

            Departments = (await DepartmentService.GetAllAsync()).ToList();

            EmployeeDepartmentId = DepartmentId;
        }

        private async Task HandleValidSubmit()
        {
            var departmentId = int.Parse(DepartmentId);
            if (!string.IsNullOrWhiteSpace(EmployeeId))
            {
                var employeeId = int.Parse(EmployeeId);
                await EmployeeService.UpdateForDepartmentAsync(departmentId, employeeId, Employee);
                
                Saved = true;
                Message = "修改成功";
                CssClass = "alert alert-success";
            }
            else
            {
                Employee.PictureUrl = "http://www.sinaimg.cn/dy/slidenews/2_img/2017_23/22616_2187093_887635.jpg";
                var employee = await EmployeeService.AddForDepartmentAsync(departmentId, Employee);
                if (employee != null)
                {
                    Saved = true;
                    Message = "新增成功";
                    CssClass = "alert alert-success";
                }
                else
                {
                    Saved = false;
                    Message = "新增失败";
                    CssClass = "alert alert-danger";
                }
            }
        }

        private void HandleInvalidSubmit()
        {
            CssClass = "alert alert-danger";
            Message = "表单验证失败";
        }

        private async Task DeleteEmployee()
        {
            await EmployeeService.DeleteFromDepartmentAsync(int.Parse(DepartmentId), int.Parse(EmployeeId));

            Saved = true;
            Message = "删除成功";
            CssClass = "alert alert-success";
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }
    }
}
