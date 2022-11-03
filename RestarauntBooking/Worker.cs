using MassTransit;

using Microsoft.Extensions.Hosting;

using Restaraunt.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaraunt.Booking
{
	public class Worker : BackgroundService
	{

		private readonly IBus _bus;

		RestarauntService _restarauntService;
		public Worker(IBus bus, RestarauntService restarauntService)
		{
			_bus = bus;

			_restarauntService = restarauntService;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			Console.OutputEncoding = Encoding.UTF8;
			while (!stoppingToken.IsCancellationRequested)
			{

				await Task.Delay(2000, stoppingToken);

				Console.WriteLine("Привет! Желаете забронировать столик?");
				var result =  _restarauntService.BookTable("1");

				if (!result.IsCanceled)
				{
					await _bus.Publish(new BookingRequest(NewId.NextGuid(), NewId.NextGuid(), Dish.Chicken,  DateTime.Now, TimeSpan.FromSeconds(new Random().Next(7, 15))),
					context => context.Durable = false, stoppingToken);
				}
				else
				{

				}
			}			
		}
	}
}
