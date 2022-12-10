using DesafioFinal.Models;
using DesafioFinal.Repositorio.SubscriptionRepo;
using DesafioFinal.Repositorio.UserRepo;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public class ConsumerMessageRepo : IConsumerMessageRepo
    {
        private readonly ConnectionFactory factory;
        private readonly ISubscriptionRepo _repositorio;
        private readonly IUserRepo _userRepo;
        public ConsumerMessageRepo(IUserRepo userRepo, ISubscriptionRepo repo)
        {
            factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            _userRepo = userRepo;
            _repositorio = repo;
        }

        public void IniciarFilaCriar()
        {
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();

            channel.QueueDeclare(queue: "CriarUser",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var order = System.Text.Json.JsonSerializer.Deserialize<User>(body);
                    try
                    {
                        Task.Delay(1000).Wait();

                        await _userRepo.Create(order);

                        Task.Delay(1000).Wait();

                        channel.BasicAck(ea.DeliveryTag, true);

                    }
                    catch (Exception ex)
                    {
                        channel.BasicNack(ea.DeliveryTag, false, true);//recolocar o item na fila e deixar disponível
                        throw new System.Exception(ex.Message);
                    }
                };

            string tag = channel.BasicConsume(queue: "CriarUser",
                                 autoAck: false,
                                 consumer: consumer);
            Task.Delay(10000).Wait();

            channel.BasicCancel(tag);
            Task.Delay(2000).Wait();

            channel.Dispose();
            Task.Delay(2000).Wait();
        }
        public void IniciarFilaDesativar()
        {
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
                        throw new System.Exception(ex.Message);
                    }
                };

                string tag = channel.BasicConsume(queue: "DesativarUser",
                                     autoAck: false,
                                     consumer: consumer);

                Task.Delay(10000).Wait();

                channel.BasicCancel(tag);
                Task.Delay(2000).Wait();

                channel.Dispose();
                Task.Delay(2000).Wait();
        }

        public void IniciarFilaReativar()
        {
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();

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
                    throw new System.Exception(ex.Message);
                }
            };

            string tag = channel.BasicConsume(queue: "ReativarUser",
                                 autoAck: false,
                                 consumer: consumer);
            Task.Delay(10000).Wait();

            channel.BasicCancel(tag);
            Task.Delay(2000).Wait();

            channel.Dispose();
            Task.Delay(2000).Wait(); ;
        }
     }
 }

