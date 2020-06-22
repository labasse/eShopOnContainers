using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

namespace eShopOnContainers.Common.EventBus
{
    public class RabbitMQEventBus : IEventBus
    {
        private IConnection _connection;
        private IModel _subscribeChannel;        
        private readonly string _queueName;

        public RabbitMQEventBus(string host, string queueName)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = host };

            _queueName = queueName;
            _connection = factory.CreateConnection();
            _subscribeChannel = null;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public void Publish<T>(T message) where T : IMessage
        {
            using(var channel = _connection.CreateModel())
            {
                var json = JsonConvert.SerializeObject(message);
                string routing = message.GetType().Name;

                channel.QueueDeclare(_queueName, false, false, false, null);
                channel.BasicPublish(
                    exchange: "",
                    routingKey: _queueName,
                    basicProperties: null,
                    body: Encoding.UTF8.GetBytes(json)
                );
            }
        }

        public void Subscribe<T>(ISubscriber<T> subscriber) where T : IMessage
        {
            if(_subscribeChannel==null)
            {
                _subscribeChannel = _connection.CreateModel();

                _subscribeChannel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            }
            var consumer = new EventingBasicConsumer(_subscribeChannel);

            consumer.Received += (sender, args) => subscriber.OnReceived(
                JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(args.Body.ToArray()))
            ).Wait();
            _subscribeChannel.BasicConsume(_queueName, true, consumer);
        }
    }
}
