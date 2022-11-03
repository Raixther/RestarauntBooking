using MassTransit;

using Microsoft.Extensions.Logging;

using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Kitchen.Consumers.Fault
{
	public class KitchenTableBookedFaultConsumer : IConsumer<Fault<ITableBooked>>
	{
		private readonly ILogger<KitchenTableBookedFaultConsumer> _logger;

		public KitchenTableBookedFaultConsumer(ILogger<KitchenTableBookedFaultConsumer> logger)
		{
			_logger = logger;
		}

		public Task Consume(ConsumeContext<Fault<ITableBooked>> context)
		{
			foreach (var item in context.Message.Exceptions)
			{
				_logger.LogError(item.Message);
			}
			return context.ConsumeCompleted;
		}
	}
}
