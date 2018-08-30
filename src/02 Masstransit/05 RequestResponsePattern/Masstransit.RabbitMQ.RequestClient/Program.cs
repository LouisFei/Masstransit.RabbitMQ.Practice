using System;
using System.Threading.Tasks;

using Masstransit.RabbitMQ.Extensions;
using Masstransit.RabbitMQ.RequestMessage;
using MassTransit;

namespace Masstransit.RabbitMQ.RequestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Enter' to send a message. To exit, Ctrl + C");

            var bus = BusCreator.CreateBus();
            bus.Start();

            //创建请求客户端
            var client = CreateRequestClient(bus);

            for (;;)
            {
                Console.Write("Enter customer id (quit exits): ");
                string customerId = Console.ReadLine();
                if (customerId == "quit")
                {
                    break;
                }

                // this is run as a Task to avoid weird console application issues
                Task.Run(async () =>
                {
                    //发起请求，获得响应
                    var response = await client.Request(new SimpleRequest() { CustomerId = customerId });

                    //打印响应信息
                    Console.WriteLine("Customer Name: {0}", response.CusomerName);
                }).Wait();
            }
        }

        /// <summary>
        /// 创建请求客户端
        /// </summary>
        /// <param name="busControl"></param>
        /// <returns></returns>
        private static IRequestClient<ISimpleRequest, ISimpleResponse> CreateRequestClient(IBusControl busControl)
        {
            var serviceAddress = new Uri($"{RabbitMqConstants.RabbitMqUri}{RabbitMqConstants.RequestClientQueue}");
            var client = busControl.CreateRequestClient<ISimpleRequest, ISimpleResponse>(serviceAddress, TimeSpan.FromSeconds(10));
            return client;
        }
    }
}
