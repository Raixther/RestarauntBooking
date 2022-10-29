using Microsoft.Extensions.Logging;

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

		private readonly ILogger<Restaraunt> _logger;
		public Restaraunt(ILogger<Restaraunt> logger)
		{
			_logger = logger;

			for (int i = 0;  i < 10;  i++)
			{
				_tables.Add(new Table(i));
			}

		} 
		#region Booking
		public bool BookFreeTable(int countOfPersons)
		{
			Console.WriteLine("Добрый день! Подождите секунду, я подберу вам столик и подтвержу вашу бронь, оставайтесь на линии");

			var table = _tables.FirstOrDefault(t => t.SeatsCount>countOfPersons&&t.State==State.Free);

			Thread.Sleep(7000);

			if (table is null )
			{
				Console.WriteLine("К сожалению все столики заняты");				
				return false;
			}
			else
			{
				table.SetState(State.Booked);
				Console.WriteLine($"Готово! Ваш столик номер {table.Id}");
				return true;
			}
		}
		public async Task<Table?> BookFreeTableAsync(int countOfPersons)
		{
			_logger.LogInformation("Test");

			CancellationToken token = new();

			var table = _tables.FirstOrDefault(t => t.SeatsCount>countOfPersons&&t.State==State.Free);
			if (table is null)
			{
				return Task.FromCanceled<Table>(token).Result;
			}
			
			await Task.Run(async () =>
			{
				await Task.Delay(3000);

			});

			table.SetState(State.Booked);

			return table;
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

