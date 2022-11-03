using MassTransit;

using Microsoft.Extensions.Logging;

using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Notification.Consumers.Fault
{
	public class NotifierKitchenReadyFaultConsumer : IConsumer<Fault<IKitchenReady>>
	{

		private readonly ILogger<NotifierKitchenReadyFaultConsumer> _logger;

		public NotifierKitchenReadyFaultConsumer(ILogger<NotifierKitchenReadyFaultConsumer> logger)
		{
			_logger = logger;
		}
		public Task Consume(ConsumeContext<Fault<IKitchenReady>> context)
		{
			foreach (var item in context.Message.Exceptions)
			{
				_logger.LogError(item.Message);
			}
			return context.ConsumeCompleted;
		}
	}
}
