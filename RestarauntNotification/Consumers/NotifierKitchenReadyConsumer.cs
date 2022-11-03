using MassTransit;

using Microsoft.Extensions.Logging;

using Restaraunt.Booking;
using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Notification.Consumers
{
	public class NotifierKitchenReadyConsumer : IConsumer<IKitchenReady>
	{
        private readonly Notifier _notifier;

        private readonly ILogger<NotifierKitchenReadyConsumer> _logger;
        public NotifierKitchenReadyConsumer(Notifier notifier, ILogger<NotifierKitchenReadyConsumer> logger)
        {
            _notifier = notifier;
            _logger = logger;

        }

        public Task Consume(ConsumeContext<IKitchenReady> context)
        {
            Console.WriteLine("5");
			if (context.Message.Ready)
			{
                _notifier.Accept(context.Message.OrderId, Accepted.Kitchen);
                return context.ConsumeCompleted;
            }
			else
            {
                _notifier.Accept(context.Message.OrderId, Accepted.Rejected);
                return context.ConsumeCompleted;
            }

         
        }
    }
}
