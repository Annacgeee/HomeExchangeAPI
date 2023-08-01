using HomeExchangeAPI.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Serilog;
using Microsoft.Extensions.Logging;
using HomeExchangeAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/homeLogs.txt",rollingInterval : RollingInterval.Day).CreateLogger();

// builder.Host.UseSerilog();
var connectionString = builder.Configuration.GetConnectionString("DefaultSQLConnection");
 Console.WriteLine($"Connection string: {connectionString}");

builder.Services.AddDbContextPool<ApplicationDbContext>(options => {
    
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));

}); 


builder.Services.AddControllers(option => {

})
.AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));

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

