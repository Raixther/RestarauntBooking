using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Restaraunt.Kitchen;
using Restaraunt.Kitchen.Consumers;

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
                        x.AddConsumer<KitchenTableBookedConsumer>();
                        x.UsingRabbitMq((context, cfg) =>
                        {
                           
                            cfg.Host("cow-01.rmq2.cloudamqp.com", "fqddpxzb", h =>
                            {
                                h.Username("fqddpxzb");
                                h.Password("1p4dj690lb4H9N03XHmrOLtXDlLGZaUf");
                            }
                        );

                            cfg.ConfigureEndpoints(context);

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