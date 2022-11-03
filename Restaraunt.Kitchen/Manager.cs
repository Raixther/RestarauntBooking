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
        public Manager()
        {
            _rand = new();
        }
        public bool CheckKitchenReady(Guid orderId, Dish? dish)
        {

            var result = _rand.Next(0, 100);


			if (dish==Dish.Lasagna)
			{
                throw new Exception();
			}

			if (result>80)
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
