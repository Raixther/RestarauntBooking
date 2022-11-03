using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Messages
{
    public interface IKitchenReady
    {
        public Guid OrderId { get; }

        public bool Ready { get; }

        public int TableId{ get; }
    }
}
