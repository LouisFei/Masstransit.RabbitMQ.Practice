using System;
using System.Threading.Tasks;

using MassTransit;
using UserManagement.Events;

namespace UserManagement.Service
{
    /// <summary>
    /// 用户创建事件消息消费者
    /// </summary>
    public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly GreetingWriter _greetingWriter;

        public UserCreatedEventConsumer()
        {
        }

        public UserCreatedEventConsumer(GreetingWriter greetingWriter)
        {
            _greetingWriter = greetingWriter;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            _greetingWriter.SayHello();

            await Console.Out.WriteLineAsync($"user name is {context.Message.UserName} from {this.GetType().ToString()}");
            await Console.Out.WriteLineAsync($"user email is {context.Message.Email} from {this.GetType().ToString()}");
        }
    }
}