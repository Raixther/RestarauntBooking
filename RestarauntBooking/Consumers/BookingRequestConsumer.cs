using MassTransit;

using Microsoft.Extensions.Logging;

using Restaraunt.Booking;
using Restaraunt.Messages;
using Restaraunt.Messages.InMemoryDb;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Kitchen.Consumers
{
	public class BookingRequestConsumer : IConsumer<BookingRequest>
	{
		private readonly IInMemoryRepository<IBookingRequest> _repository;

		private readonly RestarauntService _restarauntService;

		private readonly ILogger<BookingRequestConsumer> _logger;
		public BookingRequestConsumer(IInMemoryRepository<IBookingRequest> repository, RestarauntService restarauntService, ILogger<BookingRequestConsumer> logger)
		{
			_repository = repository;
			_restarauntService = restarauntService;
			_logger = logger;
		}

		public Task Consume(ConsumeContext<BookingRequest> context)
		{

			var savedMessage = _repository.Get()
				.FirstOrDefault(m => m.OrderId == context.Message.OrderId);

			if (savedMessage is null)
			{
				
				_repository.AddOrUpdate(context.Message);
				var result =  _restarauntService.BookTable("1");

				_logger.LogInformation($"Забронирован стол {result.Result.Id}");

				return Task.FromResult(result);
			}

			return context.ConsumeCompleted;
		}
	}
}
