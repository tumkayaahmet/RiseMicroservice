using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Report.Services;
using Report.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<ConnectionFactory>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("RabbitMQ");

    return new ConnectionFactory
    {
        Uri = new Uri(connectionString),
        DispatchConsumersAsync = true
    };
});
builder.Services.AddSingleton<RabbitMQClientService>();
//services.AddSingleton<RabbitMQPublisher>();

builder.Services.AddScoped<IReportDetailServices, ReportDetailServices>();
builder.Services.AddScoped<IReportPreparingServices, ReportPreparingServices>();
//builder.Services.AddSingleton<RabbitMQPublisher>();
builder.Services.AddSingleton<RabbitMQClientService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp => { return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value; });
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
