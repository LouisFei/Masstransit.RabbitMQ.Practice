using System;
using System.Threading.Tasks;

using MassTransit;
using UserManagement.Events;

namespace UserManagement.Service
{
    public class UserUpdatedEventComsumer
        :IConsumer<UserUpdatedEvent>
        ,IConsumer<Fault<UserUpdatedEvent>>
    {
        public Task Consume(ConsumeContext<UserUpdatedEvent> context)
        {
            throw new System.NotImplementedException();
        }

        public async Task Consume(ConsumeContext<Fault<UserUpdatedEvent>> context)
        {
            await Console.Out.WriteLineAsync($"catch exception: {context.Message.Message}");
        }
    }
}