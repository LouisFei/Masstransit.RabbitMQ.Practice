using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Masstransit.RabbitMQ.Extensions
{
    public static class BusCreator
    {
        /// <summary>
        /// 创建总线
        /// </summary>
        /// <param name="registrationAction"></param>
        /// <returns></returns>
        public static IBusControl CreateBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            //创建基于rabbitMq的总线
            return Bus.Factory.CreateUsingRabbitMq(rabbitMqBusFactoryCfg =>
            {
                var host = rabbitMqBusFactoryCfg.Host(new Uri(RabbitMqConstants.RabbitMqUri), rabbitMqHostCfg =>
                {
                    rabbitMqHostCfg.Username(RabbitMqConstants.UserName);
                    rabbitMqHostCfg.Password(RabbitMqConstants.Password);
                });

                registrationAction?.Invoke(rabbitMqBusFactoryCfg, host);
            });
        }
    }
}
