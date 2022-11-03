using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Messages
{
	public interface IBookingCancellation
	{
		public Guid ClientId { get; }

		public int TableId{ get; }
	}
}
