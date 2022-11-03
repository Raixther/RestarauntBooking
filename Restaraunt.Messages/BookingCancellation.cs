using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Messages
{
	public class BookingCancellation : IBookingCancellation
	{
		public BookingCancellation(Guid clientId, int tableId)
		{
			ClientId = clientId;
			TableId = tableId;
		}
		public Guid ClientId { get; }

		public int TableId { get; }


	}
}
