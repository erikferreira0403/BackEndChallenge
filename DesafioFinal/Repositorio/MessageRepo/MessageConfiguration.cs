using DesafioFinal.Data;
using DesafioFinal.Models;
using DesafioFinal.Repositorio.UserRepo;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public class MessageConfiguration : IMessageConfiguration
    {
        private const string QUEUE_NAME = "messages";
        private readonly ConnectionFactory _factory;
        private readonly DataContext _dataContext;
        private readonly IUserRepo _userRepo;

        public MessageConfiguration(DataContext dataContext, IUserRepo userRepo)
        {
            _factory = new ConnectionFactory
            {

                HostName = "localhost"
            };
            _dataContext = dataContext;
            _userRepo = userRepo;

        }
        public  User Enviar(User messageModel)
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

      public  async Task IniciarFila()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "game_results",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var order = System.Text.Json.JsonSerializer.Deserialize<User>(body);
                    try
                    {

                        
                         _userRepo.Create(order, order.FullName);

                        channel.BasicAck(ea.DeliveryTag, true);
                       
                    }
                    catch (Exception ex)
                    {

                        channel.BasicNack(ea.DeliveryTag, false, true);//recolocar o item na fila e deixar disponível
                       
                    }
                };

                channel.BasicConsume(queue: "game_results",
                                     autoAck: false,
                                     consumer: consumer);

               
                Console.ReadLine();
                
            }
        }
    }
}
