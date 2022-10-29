
using MassTransit;

using Microsoft.Extensions.Logging;

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

		private readonly ILogger<Restaraunt> _logger;

		public RestarauntService(Restaraunt restaraunt, ILogger<Restaraunt> logger)
		{
			_restaraunt = restaraunt;
			_logger = logger;
		}
		public Task<Table?> BookTable(string command)
		{
			CancellationToken token = new();
			int instruction = Convert.ToInt32(command);
			Console.WriteLine("Спасибо за ваше обращение!");

			switch (instruction)
			{	case 1:
					var result = _restaraunt.BookFreeTableAsync(1);

					return result;
				//case 2:
				//	var result2 = _restaraunt.BookFreeTable(1);

				//	return Task.FromResult(result2.);
						
				default:
					Console.WriteLine("Введите пожалуйста 1 или 2");
					return Task.FromCanceled<Table?>(token);
			}

		}
		public void Unbook(int tableId)
		{
			_restaraunt.Unbook(tableId);
		}
	}
}
