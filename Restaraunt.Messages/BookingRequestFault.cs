﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Messages
{
	public class BookingRequestFault : IBookingRequestFault
	{
		public Guid OrderId { get; set; }
	
	}
}
