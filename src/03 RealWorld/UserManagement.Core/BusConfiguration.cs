using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace UserManagement.Core
{
    /// <summary>
    /// 定义一个抽象类，用来统一配置。
    /// 给客户端和服务端的子类继承。
    /// </summary>
    public abstract class BusConfiguration
    {
        /// <summary>
        /// 消息队列服务地址
        /// </summary>
        public abstract string RabbitMqAddress { get; }

        /// <summary>
        /// 消息队列名称
        /// </summary>
        public abstract string QueueName { get; }

        /// <summary>
        /// 消息队列服务访问用户名
        /// </summary>
        public abstract string RabbitMqUserName { get; }

        /// <summary>
        /// 消息队列服务访问密码
        /// </summary>
        public abstract string RabbitMqPassword { get; }

        public abstract Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> Configuration { get; }

        protected BusConfiguration()
        {
            ConnectObservers = null;
        }

        /// <summary>
        /// 挂载总线监视器
        /// </summary>
        public virtual Action<IBus> ConnectObservers { get; }

        /// <summary>
        /// 创建总线
        /// </summary>
        /// <returns></returns>
        public virtual IBus CreateBus()
        {
            //创建基于RabbitMq消息队列的消息总线。
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                //配置rabbitMq消息队列。
                var host = cfg.Host(new Uri(RabbitMqAddress), hst =>
                {
                    hst.Username(RabbitMqUserName);
                    hst.Password(RabbitMqPassword);
                });

                //配置消息总线。留给具体的子类实现。
                Configuration?.Invoke(cfg, host);

                /*
                    一旦创建了总线，就会创建接收端点，无法修改。
                    然而，总线本身提供了一个临时的（自动删除）队列，可以用来接收消息。
                    为了将消费者连接到总线临时队列，可以使用一系列连接方法。

                    警告：临时队列将不接收已发布的消息。由于队列是临时的，当消费者连接时，不会创建绑定或订阅。这使得它对于临时消费者非常快，并且避免用临时绑定来击乱消息代理。

                    临时队列对于接收请求响应和故障和路由滑移事件非常有用。

                 */
            });

            //挂载总线监视器
            ConnectObservers?.Invoke(bus);

            return bus;
        }
    }
}