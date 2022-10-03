using Messaging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking
{
	public class RestarauntService
	{
		private readonly Restaraunt _restaraunt;

		private readonly Producer _producer;

		public RestarauntService()
		{
			_restaraunt = new();
			_producer = new("BookingNotifications", "cow-01.rmq2.cloudamqp.com");
		}

		public void BookTable(string command)
		{
			int instruction = Convert.ToInt32(command);

			switch (instruction)
			{	case 1:
					_restaraunt.BookFreeTableAsync(1);
					_producer.Send("");
					break;
				case 2:
					_restaraunt.BookFreeTable(1);
					break;
				default:
					Console.WriteLine("Введите пожалуйста 1 или 2");
					break;					
			}
			Console.WriteLine("Спасибо за ваше обращение!");
		}
	}
}
