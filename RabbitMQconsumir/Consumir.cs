using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQconsumir.Model;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace RabbitMQconsumir
{
    public class Consumir : BackgroundService
    {
        private readonly ILogger<Consumir> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Consumir(ILogger<Consumir> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //configurando a conexao com o Rabbitmq

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                };

                using var connection = await factory.CreateConnectionAsync();
                using (var channel = await connection.CreateChannelAsync())
                {
                    await channel.QueueDeclareAsync(
                    queue: "fila",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                    //consumindo a mensagens

                    var consumer = new AsyncEventingBasicConsumer(channel);

                    consumer.ReceivedAsync += async (sender, eventsargs) =>
                    {
                        var body = eventsargs.Body.ToArray();
                        var mensagem = Encoding.UTF8.GetString(body);
                        var pedido = JsonSerializer.Deserialize<Pedido>(mensagem);

                        using var scope = _serviceProvider.CreateScope();
                        var dbContext = scope.ServiceProvider.GetRequiredService<MeuDbContext>();

                        if (pedido != null)
                        {
                            // Adiciona ou atualiza no banco de dados
                            await dbContext.Pedidos.AddAsync(pedido);
                            await dbContext.SaveChangesAsync();
                        }

                        Console.WriteLine(pedido?.ToString());
                    };

                    //despachando a mensagem
                    await channel.BasicConsumeAsync(
                        queue: "fila",
                        autoAck: true,
                        consumer: consumer);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

