using FileCreate;
using FileCreate.Services;
using Irony.Ast;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration Configuration = hostContext.Configuration;

        services.AddSingleton<RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
