using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using User_Service.Data.Entities;
using Newtonsoft.Json;
using User_Service.Data;
using Microsoft.Extensions.DependencyInjection;

public class EventBusReceive
{
    private IConnection connection;
    private IModel channel;

    private ApplicationDbContext _context;

    public EventBusReceive(IServiceScopeFactory factory)
    {
        //_context = context;

        var scope = factory.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        ReceiveUser();
    }

    public void ReceiveUser()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: "auth_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, ea) =>
        {
            User response = new User();

            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);

            int dots = message.Split('.').Length - 1;

            //Do something with the message.
            try
            {
                Console.WriteLine(" [.] User({0})", message);
                response = JsonConvert.DeserializeObject<User>(message);

                _context.User.Add(response);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(" [.] " + e.Message);
                response = null;
            }

            Console.WriteLine(" [x] Done " + response.FirstName);
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        channel.BasicConsume(queue: "auth_queue", autoAck: false, consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        //Console.ReadLine();
        
    }
}
