
using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Restaraunt.Notification.Consumers;
using Restaraunt.Notification.Consumers.Fault;

namespace Restaraunt.Notification
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
				services.AddLogging();
				services.AddMassTransit(x=>
				{
					x.AddConsumer<NotifierTableBookedConsumer>(c =>
					{
						c.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

						c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));

					});
					x.AddConsumer<NotifierKitchenReadyConsumer>(c =>
					{
						c.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

						c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));

					});


					x.AddConsumer<NotifierTableBookedFaultConsumer>(c =>
					{
						c.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

						c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));

					}).Endpoint(e=>e.Temporary = true);
					x.AddConsumer<NotifierKitchenReadyFaultConsumer>(c =>
					{
						c.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

						c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));

					}).Endpoint(e=>e.Temporary = true);

					x.UsingRabbitMq((context, cfg)=>
					{
						cfg.Host("cow-01.rmq2.cloudamqp.com", "fqddpxzb", h =>
						{
							h.Username("fqddpxzb");
							h.Password("1p4dj690lb4H9N03XHmrOLtXDlLGZaUf");
						});
						cfg.ConfigureEndpoints(context);

						cfg.UseInMemoryOutbox();
					});

					
				});

				services.AddOptions<MassTransitHostOptions>().Configure(o=>
				{
					o.WaitUntilStarted = true;
				});
				

				services.AddSingleton<Notifier>();
			}
			);
	}
}