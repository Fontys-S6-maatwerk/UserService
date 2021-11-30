using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using User_Service.Data.Entities;

namespace User_Service.Web.Logic
{
    public class EventBusSend
    {
        public void SendUser(UserDto sendUser)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "update_user_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var message = GetMessageCreateUser(sendUser);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.CorrelationId = Guid.NewGuid().ToString();

                channel.BasicPublish(exchange: "", routingKey: "update_user_queue", basicProperties: properties, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            //Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();
        }

        private static string GetMessageCreateUser(UserDto user)
        {
            var json = JsonConvert.SerializeObject(user);
            string message = "Update User";
            return ((json.Length > 0) ? string.Join(" ", json) : message);
        }
    }
}
