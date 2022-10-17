using MassTransit;

using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking.Consumers
{
	
	public class BookingKitchenReadyConsumer : IConsumer<IKitchenReady>
	{
		private readonly RestarauntService _restarauntService;

		public BookingKitchenReadyConsumer(RestarauntService restarauntService)
		{
			_restarauntService = restarauntService;
		}
		public Task Consume(ConsumeContext<IKitchenReady> context)
		{
			if (!context.Message.Ready)
			{
				Console.WriteLine("8");
				_restarauntService.Unbook(context.Message.TableId);
				Console.WriteLine($"Нет возможности заказать стол номер{context.Message.TableId}");
			}
			return context.ConsumeCompleted;
		}
	}
}
