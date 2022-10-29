using MassTransit;

using Restaraunt.Booking.Consumers;
using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking
{
	public sealed class RestarauntBookingSaga : MassTransitStateMachine<RestarauntBooking>
	{
		public MassTransit.State AwaitingBookingApproved{ get; private set; }

		public MassTransit.State AwaitingGuest { get; private set; }// // // 
		
		public Event<GuestArrive> GuestArrive{ get; private set; }

		public Event<GuestNotArrive> GuestNotArrive { get; private set; }

		public Event<IBookingRequested> BookingRequested{ get; private set; }

		public Event<ITableBooked> TableBooked { get; private set; }

		public Event<IKitchenReady> KitchenReady { get; private set; }

		public Event<IBookingRequestFault> BookingRequestFault { get; private set; }

		public Event BookingApproved{ get; private set; }

	

		public Schedule<RestarauntBooking,IBookingExpire> BookingExpired{ get; private set; }

		public Schedule<RestarauntBooking, GuestNotArrived> GuestNotArrived { get; private set; }

		public RestarauntBookingSaga()
		{
			InstanceState(x=>x.CurrentSate);

			Event(() => GuestArrive, x => x.CorrelateById(context => context.Message.ClientId).SelectId(context => context.Message.ClientId));

			Event(() => GuestNotArrive, x => x.CorrelateById(context => context.Message.ClientId).SelectId(context => context.Message.ClientId));

			Event(() => BookingRequested, x => x.CorrelateById(context=>context.Message.OrderId).SelectId(context=>context.Message.OrderId));

			Event(() => TableBooked, x => x.CorrelateById(context=>context.Message.OrderId));

			Event(() => KitchenReady, x => x.CorrelateById(context => context.Message.OrderId));
			 
			CompositeEvent(() => BookingApproved, x => x.ReadyEventStatus, KitchenReady, TableBooked);

			Event(() => BookingRequestFault, x => x.CorrelateById(context => context.Message.OrderId));

			Schedule(()=>BookingExpired,
				x => x.ExpirationId, x =>
				{
					x.Delay =TimeSpan.FromSeconds(5);
					x.Received = e => e.CorrelateById(context => context.Message.OrderId);
				}
			);
			Schedule(() => GuestNotArrived,
				x => x.ExpirationId, x =>
				{		
					x.Received = e => e.CorrelateById(context => context.Message.ClientId);
				}
			);

			Initially(When(BookingRequested).
			Then(context =>
			{
				context.Saga.CorrelationId = context.Message.OrderId;
				context.Saga.OrderId = context.Message.OrderId;
				context.Saga.ClientId = context.Message.ClientId;
				context.Saga.WaitingTime = context.Message.WaitingTime;
				Console.WriteLine("Saga: " + context.Message.CreationTime);
			}).Schedule(BookingExpired,
						context => new BookingExpire(context.Saga),
						context => TimeSpan.FromSeconds(1))
					.TransitionTo(AwaitingBookingApproved));


			During(AwaitingBookingApproved,
			 When(BookingApproved).TransitionTo(AwaitingGuest).
				 Unschedule(BookingExpired)
				 .Publish(context =>
					 (INotify)new Notify(context.Saga.OrderId,
						 context.Saga.ClientId,
						 $"Стол успешно забронирован")).Schedule(GuestNotArrived, context => new GuestNotArrived(context.Saga), context =>context.Saga.WaitingTime)
				 .Finalize(),
			 When(BookingRequestFault)
				 .Then(context => Console.WriteLine($"Ошибочка вышла!"))
				 .Publish(context => (INotify)new Notify(context.Saga.OrderId,
					 context.Saga.ClientId,
					 $"Приносим извинения, стол забронировать не получилось."))
				 .Publish(context => (IBookingCancellation)
					 new BookingCancellation(context.Message.OrderId))
				 .Finalize(),

			 When(BookingExpired.Received)
				 .Then(context => Console.WriteLine($"Отмена заказа {context.Saga.OrderId}"))
				 .Finalize());


			During(AwaitingGuest, When(GuestArrive).Unschedule(GuestNotArrived).Finalize(),
						When(GuestNotArrive).Publish(context => (IBookingCancellation)
					 new BookingCancellation(context.Message.ClientId)).Finalize());
		}

	}
}
