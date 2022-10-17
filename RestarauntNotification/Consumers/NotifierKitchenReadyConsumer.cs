using MassTransit;

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

        public NotifierKitchenReadyConsumer(Notifier notifier)
        {
            _notifier = notifier;
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
