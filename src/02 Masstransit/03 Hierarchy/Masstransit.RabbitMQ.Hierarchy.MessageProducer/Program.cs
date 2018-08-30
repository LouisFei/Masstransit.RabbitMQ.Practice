using System;

using Masstransit.RabbitMQ.Extensions;
using Masstransit.RabbitMQ.HierarchyMessage;

namespace Masstransit.RabbitMQ.MessageProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Hierarchy message producer";

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
                var uuMsg = new UserUpdatedMessage() { Id = Guid.NewGuid(), Type = "User updated" };
                bus.Publish(uuMsg);
                Console.WriteLine($"publish command:id={uuMsg.Id},{uuMsg.Type}, {DateTime.Now}");

                var udMsg = new UserDeletedMessage() { Id = Guid.NewGuid(), Type = "User deleted" };
                bus.Publish(udMsg);
                Console.WriteLine($"publish command:id={udMsg.Id},{udMsg.Type}, {DateTime.Now}");
            }
            while (true);


            Console.WriteLine("Publish Hierarchy events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
