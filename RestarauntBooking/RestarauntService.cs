using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntBooking
{
	public class RestarauntService
	{
		private readonly Restaraunt _restaraunt;

		public RestarauntService()
		{
			_restaraunt = new();
		}

		public void BookTable(string command)
		{
			int instructioin = Convert.ToInt32(command);

			switch (instructioin)
			{	case 1:
					_restaraunt.BookFreeTableAsync(1);
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
		public static async Task Notify(string message)
		{
			await Task.Run(() =>
			{
				Task.Delay(4000);
				Console.WriteLine(message);
				return Task.CompletedTask;
			});
		}
	}
}
