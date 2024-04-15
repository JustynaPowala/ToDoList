
using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.EF;
using ToDoList.WebApi.EF.Repositories;

namespace ToDoList.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var services = builder.Services; //
			services.AddControllers(); //


			services.AddAuthorization();

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();


			services.AddScoped<ITaskItemRepository, TaskItemRepository>();

			services.AddDbContext<ToDoListDbContext>(
				option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
				);

			var app = builder.Build();



			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers(); 

			app.Run();
		}
	}
}