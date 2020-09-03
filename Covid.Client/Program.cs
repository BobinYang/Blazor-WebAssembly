using System;
using System.Net.Http;
using System.Threading.Tasks;
using Covid.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Covid.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //builder.Services.AddScoped(sp => 
            //    new HttpClient
            //    {
            //        BaseAddress = new Uri("http://localhost:7000")
            //    });
            builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(
                client => client.BaseAddress = new Uri("http://localhost:7000"));
            builder.Services.AddHttpClient<IDepartmentService, DepartmentService>(
                client => client.BaseAddress = new Uri("http://localhost:7000"));

            await builder.Build().RunAsync();
        }
    }
}
