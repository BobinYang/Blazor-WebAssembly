using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Covid.Shared.Dtos;

namespace Covid.Client.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EmployeeDto>> GetForDepartmentAsync(int departmentId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<EmployeeDto>>(
                await _httpClient.GetStreamAsync("api/department/1/employee"),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<EmployeeDto> GetOneForDepartmentAsync
            (int departmentId, int id)
        {
            return await JsonSerializer.DeserializeAsync<EmployeeDto>(
                await _httpClient.GetStreamAsync($"api/department/{departmentId}/employee/{id}"), 
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
        }

        public async Task<EmployeeDto> AddForDepartmentAsync(int departmentId, EmployeeAddOrUpdateDto employee)
        {
            var employeeJson =
                new StringContent(
                    JsonSerializer.Serialize(employee), 
                    Encoding.UTF8, 
                    "application/json");

            var response = await _httpClient.PostAsync(
                $"api/department/{departmentId}/employee", employeeJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<EmployeeDto>
                    (await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateForDepartmentAsync(int departmentId, int id, EmployeeAddOrUpdateDto employee)
        {
            var employeeJson =
                new StringContent(
                    JsonSerializer.Serialize(employee), 
                    Encoding.UTF8, 
                    "application/json");

            await _httpClient.PutAsync(
                $"api/department/{departmentId}/employee/{id}", employeeJson);
        }

        public async Task DeleteFromDepartmentAsync(int departmentId, int id)
        {
            await _httpClient.DeleteAsync($"api/department/{departmentId}/employee/{id}");
        }
    }
}
