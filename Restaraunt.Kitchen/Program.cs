using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Restaraunt.Kitchen;
using Restaraunt.Kitchen.Consumers;
using Restaraunt.Kitchen.Consumers.Fault;

//using static MassTransit.MessageHeaders;

namespace Restaurant.Kitchen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<KitchenTableBookedConsumer>(c =>
                        {
                            c.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));

                            c.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2)));

                        });

                        x.AddConsumer<KitchenTableBookedFaultConsumer>(c =>
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
                        });

                        cfg.ConfigureEndpoints(context);

                        cfg.UseInMemoryOutbox();

                        });
                    });
                    services.AddOptions<MassTransitHostOptions>().Configure(o =>
                    {
                        o.WaitUntilStarted = true;
                    });

                    services.AddSingleton<Manager>();

                });
    }
}