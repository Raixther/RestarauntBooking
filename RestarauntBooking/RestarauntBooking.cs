using MassTransit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking
{
	public class RestarauntBooking : SagaStateMachineInstance
	{
		public Guid CorrelationId { get; set; }

		public int CurrentSate{ get; set; }

		public Guid OrderId { get; set; }

		public Guid ClientId { get; set; }

		public int ReadyEventStatus{ get; set; }

		public Guid? ExpirationId{ get; set; }

		public TimeSpan WaitingTime{ get; set; }
	}
}
