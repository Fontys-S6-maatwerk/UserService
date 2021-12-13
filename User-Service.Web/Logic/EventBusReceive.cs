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
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: "auth_logs", type: ExchangeType.Fanout);

        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName,
                          exchange: "auth_logs",
                          routingKey: "");

        Console.WriteLine(" [*] Waiting for logs.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            User response = new User();
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            int dots = message.Split('.').Length - 1;
            try
            {
                Console.WriteLine("[x] Received user {0}", message);
                response = JsonConvert.DeserializeObject<User>(message);

                _context.User.Add(response);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("[.] " + e.Message);
                response = null;
            }
            Console.WriteLine(" [x] Done ", response.FirstName);
        };
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
    }
}
