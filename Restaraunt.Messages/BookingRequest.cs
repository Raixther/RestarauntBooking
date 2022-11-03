using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Messages
{
    public class BookingRequest : IBookingRequest
    {
        public BookingRequest(Guid orderId, Guid clientId, Dish? preOrder, DateTime creationTime, TimeSpan waitingTime)
        {
            OrderId = orderId;
            ClientId = clientId;
            PreOrder = preOrder;
            CreationTime = creationTime;
            WaitingTime = waitingTime;
        }

        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public Dish? PreOrder { get; }
        public DateTime CreationTime { get; }
        public TimeSpan WaitingTime { get; }
    }
}
