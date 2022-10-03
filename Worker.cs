using Messaging;

using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Notification
{
	public class Worker:BackgroundService
	{
		private readonly Consumer _consumer;

		public Worker()
		{
			_consumer = new Consumer("BookingNotifications", "cow-01.rmq2.cloudamqp.com");
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			 _consumer.Recieve((sender, args) =>
			{
				var body = args.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine("[x] Recieved {0}", message);
			});
		}
	}
}
