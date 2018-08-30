using System;
using System.Threading.Tasks;

using MassTransit;
using MassTransit.Pipeline;

namespace UserManagement.Service.Observers
{
    /// <summary>
    /// 消费者消费拦截／监视
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConsumeObserver<T> : IConsumeMessageObserver<T> where T : class
    {

        #region 消费被消费者消费前
        /// <summary>
        /// Called before a message is dispatched to any consumers.
        /// 在消息发送到任何使用者之前调用
        /// </summary>
        /// <param name="context">The consume context</param>
        /// <returns></returns>
        Task IConsumeMessageObserver<T>.PreConsume(ConsumeContext<T> context)
        {
            // called before the consumer's Consume method is called
            Console.WriteLine($"PreConsume<T> from {this.GetType().ToString()}");
            return Task.FromResult(0);
        }
        #endregion

        #region 消息被消费者消费后（注意，如果消费过程发生错误，将不会调用该方法）
        /// <summary>
        /// Called after the message has been dispatched to all consumers - 
        /// note that in the case of an exception this method is not called, and the DispatchFaulted method is called instead.
        /// 在消息被发送到所有消费者之后调用——注意，在异常情况下，不调用此方法，而是调用DispatchFaulted方法。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task IConsumeMessageObserver<T>.PostConsume(ConsumeContext<T> context)
        {
            // called after the consumer's Consume method was called
            // again, exceptions call the Fault method.
            Console.WriteLine($"PostConsume<T> from {this.GetType().ToString()}");
            return Task.FromResult(0);
        }
        #endregion

        #region 消费者消费消息时发生异常
        /// <summary>
        /// Called after the message has been dispatched to all consumers when one or more exceptions have occurred
        /// 当发生一个或多个异常时，将消息发送到所有消费者之后调用
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Task IConsumeMessageObserver<T>.ConsumeFault(ConsumeContext<T> context, Exception exception)
        {
            // called when a consumer throws an exception consuming the message
            Console.WriteLine($"ConsumeFault<T> from {this.GetType().ToString()}");
            return Task.FromResult(0);
        }
        #endregion
    }
}