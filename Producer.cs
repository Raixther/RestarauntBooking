using RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
	public class Producer
	{
		private readonly string _queueName;
		private readonly string _hostName;
		private readonly IConnection _connection;//утечка памяти ?
		private readonly IModel _channel;//


		public Producer(string queueName, string hostName)
		{
			_queueName = queueName;
			_hostName =hostName;
			var factory = new ConnectionFactory()
			{
				HostName = _hostName,
				Port = 5672,
				UserName = "fqddpxzb",
				Password = "1p4dj690lb4H9N03XHmrOLtXDlLGZaUf",
				VirtualHost ="fqddpxzb"
			};
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
		}

		public void Send(string message)
		{
		
			_channel.ExchangeDeclare(
			"direct_exchange",
			"direct",
			false,
			false,
			null
			);

			var body = Encoding.UTF8.GetBytes(message);

			_channel.BasicPublish(exchange: "direct_exchange",
			routingKey: _queueName,
			basicProperties: null,
			body:body
			);
		}

	
	}
}
