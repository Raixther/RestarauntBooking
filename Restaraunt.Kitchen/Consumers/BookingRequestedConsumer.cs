using MassTransit;

using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Kitchen.Consumers
{
	public class BookingRequestedConsumer : IConsumer<BookingRequested>
	{
		public Task Consume(ConsumeContext<BookingRequested> context)
		{
			throw new NotImplementedException();
		}
	}
}
