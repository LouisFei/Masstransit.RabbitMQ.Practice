using System;
using System.Threading.Tasks;

using MassTransit;
using Masstransit.RabbitMQ.Extensions;
using Masstransit.RabbitMQ.Greeting.Message;

namespace Masstransit.RabbitMQ.GreetingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");

            //创建消息总线
            var bus = BusCreator.CreateBus();

            //消息队列服务地址及队列名称
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}{RabbitMqConstants.GreetingQueue}");

            while (Console.ReadLine() != null)
            {
                Task.Run(() => SendCommand(bus, sendToUri)).Wait();

                Task.Run(() => SendCommand2(bus, sendToUri)).Wait();
            }

            Console.ReadLine();
        }

        /*
         发送命令模型（Send Command Pattern）
         这种模型最常见的就CQRS中的C，用来向DomainHandler发送一个Command。
         另外系统的发送邮件服务、发送短信服务也可以通过这种模式来实现。
         这种模型跟邮递员向邮箱投递邮件有点相似。
         这一模型的特点是你需要知道对方终结点的地址，意味着你要明确向哪个地址发送消息。
             */

        private static async void SendCommand(IBusControl bus, Uri sendToUri)
        {
            //向谁发送
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            //发送的内容
            var command = new GreetingCommandA()
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now
            };
            //发送
            await endPoint.Send(command);

            Console.WriteLine($"send command:id={command.Id},{command.DateTime}");
        }

        private static async void SendCommand2(IBusControl bus, Uri sendToUri)
        {
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            var command = new GreetingCommandB()
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now
            };

            await endPoint.Send(command);

            Console.WriteLine($"send command2:id={command.Id},{command.DateTime}");
        }

    }
}
