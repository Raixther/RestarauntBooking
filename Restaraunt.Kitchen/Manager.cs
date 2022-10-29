using MassTransit;
using Microsoft.Extensions.Logging;


using Restaraunt.Booking;
using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Kitchen
{
   public class Manager
    {
        
        private readonly Random _rand;

        private readonly ILogger<Manager> _logger;
        public Manager(ILogger<Manager> logger)
        {
            _rand = new();
            _logger = logger;
        }
        public bool CheckKitchenReady(Guid orderId, Dish? dish)
        {

            var result = _rand.Next(0, 100);

			if (result>90)
			{        
                return true;
            }
			else
			{
                return false;
            }
        }
    }
}
