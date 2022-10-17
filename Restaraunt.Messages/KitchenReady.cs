using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Restaraunt.Messages
{
	public class KitchenReady:IKitchenReady
    {
        public KitchenReady(Guid orderId, bool ready, int tableId)
        {
            OrderId = orderId;
            Ready = ready;
            TableId = tableId;
        }
        public Guid OrderId { get; }
        public bool Ready { get; }

		public int TableId { get; }
	}
}
