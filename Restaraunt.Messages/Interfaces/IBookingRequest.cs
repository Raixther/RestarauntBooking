using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Messages
{
	public interface IBookingRequest	
	{
        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public Dish? PreOrder { get; }

        public DateTime CreationTime { get; }

        TimeSpan WaitingTime { get; }
    }
}
