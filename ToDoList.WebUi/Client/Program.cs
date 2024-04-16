using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoList.WebUi;
using ToDoList.WebUi.ApiClients;

namespace ToDoList.WebUi
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");
			var services = builder.Services;

			services.AddBlazoredToast();
			services.AddSingleton<IToDoListApiClient, HttpToDoListApiClient>();
            services.AddTransient<ErrorHandlingHttpMessageHandler>();


            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			services
			   .AddHttpClient("ToDoListHttpClient", (sp, client) =>
			   {
				   var configuration = sp.GetRequiredService<IConfiguration>();
				   client.BaseAddress = new Uri(configuration["HttpClients:ToDoList"]);
			   })
			   .AddHttpMessageHandler<ErrorHandlingHttpMessageHandler>();
			await builder.Build().RunAsync();
		}
	}
}