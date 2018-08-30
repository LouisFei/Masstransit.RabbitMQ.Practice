using System;
using System.Threading.Tasks;

using Masstransit.RabbitMQ.RequestMessage;
using MassTransit;

namespace Masstransit.RabbitMQ.RequestService
{
    public class RequestConsumer : IConsumer<ISimpleRequest>
    {
        public async Task Consume(ConsumeContext<ISimpleRequest> context)
        {
            await Console.Out.WriteLineAsync($"recieved request:{context.Message.CustomerId}");
            context.Respond(new SimpleResponse
            {
                CusomerName = $"Hello,{context.Message.CustomerId}"
            });
        }
    }
}