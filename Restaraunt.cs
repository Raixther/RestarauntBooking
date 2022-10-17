using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking
{
	public class Restaraunt
	{
		private readonly List<Table> _tables = new();

		public Restaraunt()
		{
			for (int i = 0;  i < 10;  i++)
			{
				_tables.Add(new Table(i));
			}
		} 
		#region Booking
		public void BookFreeTable(int countOfPersons)
		{
			Console.WriteLine("Добрый день! Подождите секунду, я подберу вам столик и подтвержу вашу бронь, оставайтесь на линии");

			var table = _tables.FirstOrDefault(t => t.SeatsCount>countOfPersons&&t.State==State.Free);

			Thread.Sleep(7000);

			if (table is null )
			{
				Console.WriteLine("К сожалению все столики заняты");
			}
			else
			{
				table.SetState(State.Booked);
				Console.WriteLine($"Готово! Ваш столик номер {table.Id}");				
			}
		}
		public async void BookFreeTableAsync(int countOfPersons)
		{
			
			await Task.Run(async () =>
			{

				var table = _tables.FirstOrDefault(t => t.SeatsCount>countOfPersons&&t.State==State.Free);

				Task.Delay(7000);
			});
		}
			#endregion

		public void Unbook(int tableId)
		{
			var table = _tables.Find(t => t.Id == tableId);
			if (table is null)
			{
				Console.WriteLine("Столика с данным id не существует");
				return;
			}
			var result = table.SetState(State.Free);
			if (result == true)
			{
				Console.WriteLine("Заказ столика отменен");
			}
			else
			{
				Console.WriteLine("Столик уже свободен");
			}
		}
		//public async void UnbookAsync(int tableId)
		//{
			
		
		//}
	}
}

