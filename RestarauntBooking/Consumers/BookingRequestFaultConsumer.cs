using MassTransit;
using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking.Consumers
{
	public class BookingRequestFaultConsumer: IConsumer<Fault<IBookingRequested>>
	{
			
		public Task Consume(ConsumeContext<Fault<IBookingRequested>> context)
		{
			Console.WriteLine($"[OrderId {context.Message.Message.OrderId}] Отмена в зале");
			return Task.CompletedTask;
		}
	}
}
