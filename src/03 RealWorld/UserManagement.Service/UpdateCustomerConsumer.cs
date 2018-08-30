using System;
using System.Threading.Tasks;
using MassTransit;
using UserManagement.Core;

namespace UserManagement.Service
{
    /*
        创建消息消费者
        一个消息消费者是一个可以消费一个或多个消息类型的类，指定IConsumer<T>接口，T为消息类型。 

        当消费者订阅接收端点时，由端点接收消费者所消费的消息。
        创建一个消费者实例（using a consumer factory, which is covered），然后，通过Consume方法将消息（包裹在ConsumeContext）传递给消息者。

        Consume方法是异步的，并返回一个task。
        MassTransit等待该任务，在此期间消息对其他接收端点不可用。
        如果Consume方法成功完成（RanToCompletion的task状态）,则消息将被确认并从队列中删除。

        注意：如果消费者错误（例如抛出异常，导致Faulted的任务状态）,或者以某种方式被取消cancelled（被取消的Canceled任务状态）,则异常被传播回管道，
        在那里它可以最终被重试或移动到错误队列。




    */

    public class UpdateCustomerConsumer : IConsumer<UpdateCustomerAddress>
    {
        public async Task Consume(ConsumeContext<UpdateCustomerAddress> context)
        {
            await Console.Out.WriteLineAsync($"Updating customer: {context.Message.CustomerId}");
        }
    }
}
