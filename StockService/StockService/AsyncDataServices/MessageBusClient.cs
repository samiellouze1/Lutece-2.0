using RabbitMQ.Client;
using StockService.Data.DTOs;
using System.Text;
using System.Text.Json;

namespace StockService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public MessageBusClient(IConfiguration config)
        {
            _config = config;
            var factory = new ConnectionFactory() { HostName = _config["RabbitMQHost"], Port = int.Parse(_config["RabbitMQPort"]) };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectiononShutDown;
                Console.WriteLine("Connected to message bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ! ! ! Could not connect to message bus {ex.Message}");
            }
        }
        public void PublishNewStock(StockPublishDTO stockPublishDTO)
        {
            var message = JsonSerializer.Serialize(stockPublishDTO);
            if (_connection.IsOpen)
            {
                Console.WriteLine("RabbitMQ connection open; sending message ......");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ connection closed, not sending mesesage");
            }
        }
        private void SendMessage(string messsage)
        {
            var body = Encoding.UTF8.GetBytes(messsage);
            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"---------we have sent {messsage}");
        }
        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
            }
            Console.WriteLine("MessageBus Disposed");
        }
        private void RabbitMQ_ConnectiononShutDown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine(" ? ? ? Rabbit MQ Connection shutdown");
        }
    }
}
