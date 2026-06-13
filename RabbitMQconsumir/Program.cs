using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQconsumir;

var builder = Host.CreateApplicationBuilder(args);

// Configura o SQLite como o banco de dados do projeto
builder.Services.AddDbContext<MeuDbContext>(options =>
    options.UseSqlite("Data Source=banco.db"));

builder.Services.AddHostedService<Consumir>();

var host = builder.Build();
host.Run();
