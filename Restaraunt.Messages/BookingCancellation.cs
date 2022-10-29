﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Messages
{
	public class BookingCancellation:IBookingCancellation
	{
		public BookingCancellation(Guid orderId)
		{
			OrderId = orderId;
		}
		public Guid OrderId { get; }
	}
}