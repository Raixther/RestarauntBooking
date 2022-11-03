using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking.Consumers
{
	public class GuestNotArrived
	{
        private readonly RestarauntBooking _instance;

        public GuestNotArrived(RestarauntBooking instance)
        {
            _instance = instance;
        }
        public Guid ClientId => _instance.ClientId;
    }
}
