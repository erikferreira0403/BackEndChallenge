using DesafioFinal.Data;
using DesafioFinal.Models;
using DesafioFinal.Repositorio.SubscriptionRepo;
using DesafioFinal.Repositorio.UserRepo;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public class MessageConfiguration : IMessageConfiguration
    {
        private const string QUEUE_NAME = "messages";
        private readonly ConnectionFactory _factory;
        private readonly ISubscriptionRepo _repositorio;
        private readonly IUserRepo _userRepo;

        public MessageConfiguration(DataContext dataContext, IUserRepo userRepo, ISubscriptionRepo repo)
        {
            _factory = new ConnectionFactory
            {

                HostName = "rabbitmq"
            };
            _userRepo = userRepo;
            _repositorio = repo;


        }

        public User Enviar(User messageModel)
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

        public async Task IniciarFilas()
        {

            ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();

            channel.QueueDeclare(queue: "CriarUser",
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
                        Task.Delay(1000).Wait();

                        _userRepo.Create(order);

                        Task.Delay(1000).Wait();

                        channel.BasicAck(ea.DeliveryTag, true);

                    }
                    catch (Exception ex)
                    {

                        channel.BasicNack(ea.DeliveryTag, false, true);//recolocar o item na fila e deixar disponível

                    }
                };

                channel.BasicConsume(queue: "CriarUser",
                                     autoAck: false,
                                     consumer: consumer);


                Console.ReadLine();
        }
        public async Task IniciarFilaDesativar()
        {
            for (int i = 0; i < 4; i++)
            {
                Task.Delay(1000).Wait();
            }

            Task.Delay(1000).Wait();

            ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();
            
                channel.QueueDeclare(queue: "DesativarUser",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var order = System.Text.Json.JsonSerializer.Deserialize<Status>(body);
                    try
                    {
                        Task.Delay(1000).Wait();

                        _repositorio.Desativar(order);

                        Task.Delay(1000).Wait();

                        channel.BasicAck(ea.DeliveryTag, true);

                    }
                    catch (Exception ex)
                    {

                        channel.BasicNack(ea.DeliveryTag, false, true);//recolocar o item na fila e deixar disponível

                    }
                };

                channel.BasicConsume(queue: "DesativarUser",
                                     autoAck: false,
                                     consumer: consumer);


                Console.ReadLine();


        }

        public async Task IniciarFilaReativar()
        {
            
            for (int i = 0; i < 4; i++)
            {
                Task.Delay(1000).Wait();
            }

            Task.Delay(1000).Wait();

            ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "ReativarUser",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var order = System.Text.Json.JsonSerializer.Deserialize<Status>(body);
                    try
                    {

                        Task.Delay(1000).Wait();

                        _repositorio.Reativar(order);

                        Task.Delay(1000).Wait();

                        channel.BasicAck(ea.DeliveryTag, true);

                    }
                    catch (Exception ex)
                    {

                        channel.BasicNack(ea.DeliveryTag, false, true);//recolocar o item na fila e deixar disponível

                    }
                };

                channel.BasicConsume(queue: "ReativarUser",
                                     autoAck: false,
                                     consumer: consumer);


                    Console.ReadLine();

            }
        }
    }
}
