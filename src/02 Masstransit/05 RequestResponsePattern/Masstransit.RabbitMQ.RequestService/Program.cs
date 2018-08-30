using System;
using Masstransit.RabbitMQ.Extensions;
using MassTransit;

namespace Masstransit.RabbitMQ.RequestService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Request Service 提供服务:");
            var bus = BusCreator.CreateBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.RequestClientQueue, e =>
                {
                    e.Consumer<RequestConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Listening for Request.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
