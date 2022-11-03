using MassTransit;

using Microsoft.Extensions.Logging;

using Restaraunt.Messages;
using Restaraunt.Messages.InMemoryDb;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking.Consumers
{
	public class BookingCancellationConsumer : IConsumer<IBookingCancellation>
	{

		private readonly RestarauntService _restarauntService;

		private readonly ILogger<BookingCancellationConsumer> _logger;

		

		public BookingCancellationConsumer(RestarauntService restarauntService, ILogger<BookingCancellationConsumer> logger)
		{
			_restarauntService = restarauntService;
			_logger = logger;

		}
		public Task Consume(ConsumeContext<IBookingCancellation> context)
		{

			var result = _restarauntService.Unbook(context.Message.TableId);

			_logger.LogInformation($"Заказ столика{result} отменен");

			return context.ConsumeCompleted;
		}
	}
}
