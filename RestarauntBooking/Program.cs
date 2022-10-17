using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Restaraunt.Booking.Consumers;

namespace Restaraunt.Booking
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			CreateHostBuilder(args).Build().Run();

		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
			{
				services.AddMassTransit(x =>
				{
					x.AddConsumer<BookingKitchenReadyConsumer>();
					x.UsingRabbitMq((context, cfg) =>
					{
						cfg.Host("cow-01.rmq2.cloudamqp.com", "fqddpxzb", h =>
						{
							h.Username("fqddpxzb");
							h.Password("1p4dj690lb4H9N03XHmrOLtXDlLGZaUf");
						}
					);
					});		
				});
				services.AddOptions<MassTransitHostOptions>().Configure(o =>
				{
					o.WaitUntilStarted = true;
				});

				services.AddTransient<RestarauntService>();

				services.AddTransient<Restaraunt>();

				services.AddHostedService<Worker>();

			});

	};
}
