using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Restaraunt.Booking.Consumers;
using Restaraunt.Kitchen.Consumers;
using Restaraunt.Booking.Consumers.Fault;


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
					x.AddConsumer<BookingKitchenReadyConsumer>(c => 
						{
							c.UseScheduledRedelivery(r=> r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

							c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));					
						}
						
					);

					x.AddConsumer<BookingRequestConsumer>(c =>
					{
						c.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

						c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));

					});

					x.AddConsumer<BookingCancellationConsumer>(c =>
					{
						c.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

						c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));

					});


					x.AddConsumer<BookingRequestFaultConsumer>(c =>
					{
						c.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

						c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));

					}).Endpoint(e=>e.Temporary = true);

					x.AddConsumer<BookingKitchenReadyFaultConsumer>(c =>
					{
						c.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

						c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));

					}).Endpoint(e=>e.Temporary = true);

					x.UsingRabbitMq((context, cfg) =>
					{
						cfg.Host("cow-01.rmq2.cloudamqp.com", "fqddpxzb", h =>
						{
							h.Username("fqddpxzb");
							h.Password("1p4dj690lb4H9N03XHmrOLtXDlLGZaUf");
						}) ;

						cfg.UseInMemoryOutbox();
					});

					x.AddSagaStateMachine<RestarauntBookingSaga, RestarauntBooking>().InMemoryRepository().Endpoint(x => x.Temporary = true);

					x.AddDelayedMessageScheduler();
				});
				services.AddOptions<MassTransitHostOptions>().Configure(o =>
				{
					o.WaitUntilStarted = true;
				});

				services.AddTransient<RestarauntService>();

				services.AddTransient<Restaraunt>();

				services.AddTransient<RestarauntBooking>();
				services.AddTransient<RestarauntBookingSaga>();

				services.AddHostedService<Worker>();

			});

	};
}
