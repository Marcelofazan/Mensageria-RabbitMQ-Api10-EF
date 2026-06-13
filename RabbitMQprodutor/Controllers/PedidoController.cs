using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQprodutor.Model;
using System.Text;
using System.Text.Json;

namespace RabbitMQprodutor.Controllers
{
    [ApiController]
    [Route("/Pedido")]
    public class PedidoController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post()
        {

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            {
                await channel.QueueDeclareAsync(
                    queue: "fila",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                //publicando a mensagem

                var message = JsonSerializer.Serialize(new Pedido(0, new Usuario(0, "Marcelo", "marcelo@email.com"), DateTime.Now));

                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: "fila",
                    mandatory: false, // Novo parâmetro recomendado na versão assíncrona
                    basicProperties: new RabbitMQ.Client.BasicProperties(), // Propriedades padrão vazias
                    body: body);

            }

            return Ok();
        }
    }
}
