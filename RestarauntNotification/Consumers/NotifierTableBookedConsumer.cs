using MassTransit;

using Microsoft.Extensions.Logging;

using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Notification.Consumers
{
	public class NotifierTableBookedConsumer : IConsumer<ITableBooked>
	{
		private readonly Notifier _notifier;

		private readonly ILogger<NotifierTableBookedConsumer> _logger;

		public NotifierTableBookedConsumer(Notifier notifier, ILogger<NotifierTableBookedConsumer> logger)
		{
			_notifier = notifier;
			_logger = logger;
		}
		public Task Consume(ConsumeContext<ITableBooked> context)
		{
			Console.WriteLine("6");
			var result = context.Message.Success;

			_notifier.Accept(context.Message.OrderId, result ? Accepted.Booking : Accepted.Rejected,
				context.Message.ClientId);

			return Task.CompletedTask;
		}
	}
}
