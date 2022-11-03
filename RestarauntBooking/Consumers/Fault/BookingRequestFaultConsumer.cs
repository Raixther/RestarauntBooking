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
	public class BookingRequestFaultConsumer: IConsumer<Fault<IBookingRequest>>
	{
		private readonly ILogger<BookingRequestFaultConsumer> _logger;

		public BookingRequestFaultConsumer(ILogger<BookingRequestFaultConsumer> logger)
		{
			_logger = logger;
		}

		public Task Consume(ConsumeContext<Fault<IBookingRequest>> context)
		{
			Console.WriteLine($"[OrderId {context.Message.Message.OrderId}] Отмена в зале");

			foreach (var item in context.Message.Exceptions)
			{
				_logger.LogError(item.Message);
			}


			return context.ConsumeCompleted;

		}
	}
}
