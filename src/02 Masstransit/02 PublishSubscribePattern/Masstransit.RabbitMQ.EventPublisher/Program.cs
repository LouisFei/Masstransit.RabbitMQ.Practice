using System;
using Masstransit.RabbitMQ.Event;
using Masstransit.RabbitMQ.Extensions;

namespace Masstransit.RabbitMQ.EventPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Greeting";

            var bus = BusCreator.CreateBus();
            bus.Start();

            do
            {
                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                //发布消息
                var commandA = new GreetingEventA() { Id = Guid.NewGuid(), DateTime = DateTime.Now };
                bus.Publish(commandA);
                Console.WriteLine($"publish command:id={commandA.Id},{commandA.DateTime}");

                var commandB = new GreetingEventB() { Id = Guid.NewGuid(), DateTime = DateTime.Now };
                bus.Publish(commandB);
                Console.WriteLine($"publish command:id={commandB.Id},{commandB.DateTime}");
            }
            while (true);


            Console.WriteLine("Publish Greeting events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
