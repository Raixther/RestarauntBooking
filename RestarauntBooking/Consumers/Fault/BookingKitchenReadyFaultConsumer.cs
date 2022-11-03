using MassTransit;

using Microsoft.Extensions.Logging;

using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking.Consumers.Fault
{
	public class BookingKitchenReadyFaultConsumer : IConsumer<Fault<IKitchenReady>>
	{

		private readonly ILogger<BookingKitchenReadyFaultConsumer> _logger;

		public BookingKitchenReadyFaultConsumer(ILogger<BookingKitchenReadyFaultConsumer> logger)
		{
			_logger = logger;
		}
		public Task Consume(ConsumeContext<Fault<IKitchenReady>> context )
		{
			foreach (var item in context.Message.Exceptions)
			{
				_logger.LogError(item.Message);
			}
			return context.ConsumeCompleted;
		}
	}
}
