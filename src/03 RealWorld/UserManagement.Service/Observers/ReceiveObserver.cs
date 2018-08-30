using System;
using System.Threading.Tasks;

using MassTransit;

namespace UserManagement.Service.Observers
{
    /// <summary>
    /// 消息接收观察者。
    /// 可以监视接收端点以跟踪端点级别上的消息。
    /// </summary>
    public class ReceiveObserver : IReceiveObserver
    {
        #region 接收消费时发生错误
        /// <summary>
        /// Called when the transport receive faults
        /// 当传输接收到错误时调用
        /// </summary>
        /// <param name="context">The receive context of the message</param>
        /// <param name="exception">The exception that was thrown</param>
        /// <returns></returns>
        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            // called when an exception occurs early in the message processing, such as deserialization, etc.
            Console.WriteLine($"ReceiveFault from {this.GetType().ToString()}");
            return Task.FromResult(0);
        }
        #endregion

        #region 接收到消息时
        /// <summary>
        /// Called when a message has been delivered by the transport is about to be received  by the endpoint.
        /// 当传输发送完消息后，端点将接收到该消息时调用。
        /// </summary>
        /// <param name="context">
        /// The receive context of the message
        /// 消息的接收上下文
        /// </param>
        /// <returns></returns>
        public Task PreReceive(ReceiveContext context)
        {
            // called immediately after the message was delivery by the transport
            Console.WriteLine($"PreReceive from {this.GetType().ToString()}");

            return Task.FromResult(0);
        }
        #endregion

        #region 接收并确认消息后
        /// <summary>
        /// Called when the message has been received and acknowledged on the transport
        /// 在传输上接收并确认消息后调用
        /// </summary>
        /// <param name="context">The receive context of the message</param>
        /// <returns></returns>
        public Task PostReceive(ReceiveContext context)
        {
            // called after the message has been received and processed
            Console.WriteLine($"PostReceive from {this.GetType().ToString()}");
            return Task.FromResult(0);
        }
        #endregion

        #region 消费者消费一条消息时
        /// <summary>
        /// Called when a message has been consumed by a consumer
        /// 当使用者使用了一条消息时调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The message consume context</param>
        /// <param name="duration">The consumer duration</param>
        /// <param name="consumerType">The consumer type</param>
        /// <returns></returns>
        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
            where T : class
        {
            // called when the message was consumed, once for each consumer
            Console.WriteLine($"PostConsume from {this.GetType().ToString()}");
            return Task.FromResult(0);
        }
        #endregion

        #region 消费者消费消息发生错误时
        /// <summary>
        /// Called when a message being consumed produced a fault
        /// 当使用的消息产生错误时调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The message consume context</param>
        /// <param name="elapsed">The consumer duration</param>
        /// <param name="consumerType">The consumer type</param>
        /// <param name="exception">The exception from the consumer</param>
        /// <returns></returns>
        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan elapsed, string consumerType, Exception exception) where T : class
        {
            // called when the message is consumed but the consumer throws an exception
            Console.WriteLine($"ConsumeFault from {this.GetType().ToString()}");
            return Task.FromResult(0);
        }
        #endregion
        
    }
}