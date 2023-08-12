using BH.Api.Service;
using Microsoft.Azure.Cosmos;

namespace BH.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IEmployeeRepository>(options =>
            {
                string url = builder.Configuration.GetValue<string>("Cosmos_Url");
                string primaryKey = builder.Configuration.GetValue<string>("Cosmos_PrimaryKey");
                string dbName = builder.Configuration.GetValue<string>("Cosmos_DatabaseName");
                string containerName = builder.Configuration.GetValue<string>("Cosmos_ContainerName");

                var cosmosClient = new CosmosClient(
                    url,
                    primaryKey
                );

                return new EmployeeRepository(cosmosClient, dbName, containerName);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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