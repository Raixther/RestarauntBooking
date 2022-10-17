using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
	public class Consumer : IDisposable
	{

		private readonly string _queueName;
		private readonly string _hostName;
		private readonly IConnection _connection;
		private readonly IModel _channel;

		public Consumer(string queueName, string hostName)
		{
			_queueName = queueName;
			_hostName = hostName;
			var factory = new ConnectionFactory()
			{
				HostName = _hostName,
				Port = 5672,
				UserName ="fqddpxzb",//
				Password ="1p4dj690lb4H9N03XHmrOLtXDlLGZaUf",//
				VirtualHost ="fqddpxzb"//
			};
			_connection =factory.CreateConnection();
			_channel = _connection.CreateModel();
		}

		public void Recieve(EventHandler<BasicDeliverEventArgs> recieveCallback )
		{
			_channel.ExchangeDeclare(exchange: "direct_exchange", type:"direct");

			_channel.QueueDeclare(_queueName,
			false,
			false,
			false,
			null
			);

			_channel.QueueBind(_queueName, "direct_exchange", _queueName);

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += recieveCallback;

			_channel.BasicConsume(_queueName, true, consumer);

		}

		public void Dispose()
		{
			_connection?.Dispose();
			_channel?.Dispose();

		}

	}
}
