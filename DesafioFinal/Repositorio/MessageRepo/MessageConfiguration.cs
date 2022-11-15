using DesafioFinal.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public class MessageConfiguration : IMessageConfiguration
    {
        private const string QUEUE_NAME = "messages";
        private readonly ConnectionFactory _factory;
        public MessageConfiguration()
        {
            _factory = new ConnectionFactory
            {

                HostName = "localhost"
            };
            
        }
        public MessageModel Enviar(MessageModel messageModel)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Declara fila para que caso ela ainda não exista, eu crie ela.
                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    //modelar os dados para envio para a fila
                    var stringMessage =
                        JsonSerializer.Serialize(messageModel);
                    var byteArray = Encoding.UTF8.GetBytes(stringMessage);

                    channel.BasicPublish(
                       exchange: "",
                       routingKey: "hello",
                       basicProperties: null,
                       body: byteArray
                        );
                }
            }
            return messageModel;
        }
    }
}
