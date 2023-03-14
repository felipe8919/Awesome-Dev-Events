
using AwesomeDevEvents.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AwesomeDevEvents.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DevEventsCs");

            //Configuração para usar o banco em memória. 
            //builder.Services.AddDbContext<DevEventsDbContext>(o => o.UseInMemoryDatabase("DevEventsDb"));

            builder.Services.AddDbContext<DevEventsDbContext>(o => o.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AwesomeDevEvents.API",
                    Version = "v1",
                    Contact = new OpenApiContact 
                    {
                        Name = "Felipe Rodrigues",
                        Email = "felipegomesrodrigues19@gmail.com",
                        Url= new Uri("https://www.linkedin.com/in/felipegomesrodrigues/")                        
                    }
                });

                var xmlFile = "AwesomeDevEvents.API.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}