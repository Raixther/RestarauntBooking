using System.Threading.Tasks;

using MassTransit;

using Restaraunt.Messages;

namespace Restaraunt.Kitchen.Consumers
{
     public class KitchenTableBookedConsumer : IConsumer<ITableBooked>
    {
        private readonly Manager _manager;

        private readonly IBus _bus;

        public KitchenTableBookedConsumer(Manager manager, IBus bus)
        {
            _manager = manager;
            _bus = bus;
        }

        public Task Consume(ConsumeContext<ITableBooked> context)
        {
            Console.WriteLine("4");
            var result = context.Message.Success;

            if (result)
                _manager.CheckKitchenReady(context.Message.OrderId, context.Message.PreOrder);

			if (_manager.CheckKitchenReady(context.Message.OrderId, null))
			{
                _bus.Publish<IKitchenReady>(new KitchenReady(context.Message.OrderId, true, context.Message.TableId));
                return context.ConsumeCompleted;
            }
			else
			{
                Console.WriteLine("7");
                _bus.Publish<IKitchenReady>(new KitchenReady(context.Message.OrderId, false, context.Message.TableId));
                return context.ConsumeCompleted;
            }         
        }
    }
}