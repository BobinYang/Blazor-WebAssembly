using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Covid.Shared.Dtos;

namespace Covid.Client.Services
{
    public class DepartmentService: IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<DepartmentDto>>(
                await _httpClient.GetStreamAsync("api/department"),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

    }
}
