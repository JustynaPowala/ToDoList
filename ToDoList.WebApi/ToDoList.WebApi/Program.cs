
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

			services.AddMvc(mvcOptions =>
			{
				mvcOptions.Filters.Add(new ExceptionFilter());
			});

       

            services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedCorsOrigins",

                    corsPolicyBuilder =>
                    {
                        corsPolicyBuilder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    }
                    );
            });

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
            app.UseCors("AllowedCorsOrigins");


            app.MapControllers();


			var scope = app.Services.CreateScope();
			var dbContext = scope.ServiceProvider.GetService<ToDoListDbContext>();
			var pendingMigrations = dbContext.Database.GetPendingMigrations();

			if (pendingMigrations.Any())
			{
				dbContext.Database.Migrate();
			}
			app.Run();
		}
	}
}