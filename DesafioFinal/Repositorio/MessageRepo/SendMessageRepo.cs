using DesafioFinal.Models;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public class SendMessageRepo : ISendMessageRepo
    {
        private readonly ConnectionFactory factory;
        public SendMessageRepo()
        {
            factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }
        public User CriarUsuário(User messageModel)
        {
            using (var connection = factory.CreateConnection())
            {
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "CriarUser", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var stringMessage =
                 JsonSerializer.Serialize(messageModel);
                var byteArray = Encoding.UTF8.GetBytes(stringMessage);
                try
                {
                    channel.BasicPublish(exchange: "", routingKey: "CriarUser", basicProperties: null, body: byteArray);
                }
                catch (Exception ex)
                {
                    throw new System.Exception(ex.Message);
                }
            }
            return messageModel;
        }

        public Status DesativarUsuário(int Id, Status status)
        {
            status.Id = Id;
            using (var connection = factory.CreateConnection())
            {
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "DesativarUser", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var stringMessage =
                JsonSerializer.Serialize(status);
                var byteArray = Encoding.UTF8.GetBytes(stringMessage);

                try
                {
                    channel.BasicPublish(exchange: "", routingKey: "DesativarUser", basicProperties: null, body: byteArray);
                }
                catch (Exception ex)
                {
                    throw new System.Exception(ex.Message);
                }
            }
            return status;
        }

        public Status ReativarUsuário(int Id, Status status)
        {
            status.Id = Id;
            using (var connection = factory.CreateConnection())
            {
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "ReativarUser", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var stringMessage =
                JsonSerializer.Serialize(status);
                var byteArray = Encoding.UTF8.GetBytes(stringMessage);

                try
                {
                    channel.BasicPublish(exchange: "", routingKey: "ReativarUser", basicProperties: null, body: byteArray);
                }
                catch (Exception ex)
                {
                    throw new System.Exception(ex.Message);
                }
            }
            return status;
        }
    }
}
