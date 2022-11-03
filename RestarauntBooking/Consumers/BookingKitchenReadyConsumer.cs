using MassTransit;

using Microsoft.Extensions.Logging;

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

		private readonly ILogger<BookingKitchenReadyConsumer> _logger;

		public BookingKitchenReadyConsumer(RestarauntService restarauntService, ILogger<BookingKitchenReadyConsumer> logger)
		{
			_restarauntService = restarauntService;
			_logger = logger;
		}
		public Task Consume(ConsumeContext<IKitchenReady> context)
		{
			if (!context.Message.Ready)
			{
				_restarauntService.Unbook(context.Message.TableId);
				Console.WriteLine($"Нет возможности заказать стол номер{context.Message.TableId}");
				_logger.LogInformation($"Нет возможности заказать стол номер{context.Message.TableId}");
			}
			else
			{
				_restarauntService.BookTable("1");
				_logger.LogInformation($"Cтол номер{context.Message.TableId} доступен");
			}
			return context.ConsumeCompleted;
		}
	}
}
