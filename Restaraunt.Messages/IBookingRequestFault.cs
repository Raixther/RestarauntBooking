using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Messages
{
	public interface IBookingRequestFault
	{
		public Guid OrderId { get; set; }

	}
}
