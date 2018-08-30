using System;
using MassTransit;
using MassTransit.RabbitMqTransport;
using UserManagement.Core;

namespace UserManagement.ServiceBus
{
    /// <summary>
    /// 用户管理总线配置
    /// </summary>
    public class UserManagementBusConfiguration : BusConfiguration
    {
        /// <summary>
        /// 私有化类的构造函数，使类不可以在外部实例化，单例模式的前提。
        /// </summary>
        private UserManagementBusConfiguration() { }

        /// <summary>
        /// 消息总线使用的消息队列服务地址
        /// </summary>
        public override string RabbitMqAddress { get; } = "rabbitmq://localhost/";

        /// <summary>
        /// 消息总线使用的消息队列名称
        /// </summary>
        public override string QueueName { get; } = "UserManagementQueue";

        /// <summary>
        /// 消息总线使用的消息队列服务 用户名
        /// </summary>
        public override string RabbitMqUserName { get; } = "guest";

        /// <summary>
        /// 消息总线使用的消息队列服务 密码
        /// </summary>
        public override string RabbitMqPassword { get; } = "guest";

        /// <summary>
        /// 配置
        /// </summary>
        public override Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> Configuration
        {
            get
            {
                return (cfg, host) =>
                {
                    cfg.UseRetry(Retry.Interval(3, TimeSpan.FromMinutes(1)));
                    cfg.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                    });
                };
            }
        }

        private static IBus _bus;

        /// <summary>
        /// 总线单例
        /// </summary>
        public static IBus BusInstance
        {
            get
            {
                if (_bus == null)
                {
                    _bus = new UserManagementBusConfiguration().CreateBus();
                }

                return _bus;
            }
        }
    }
}