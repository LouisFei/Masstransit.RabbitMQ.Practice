namespace Masstransit.RabbitMQ.Extensions
{
    public class RabbitMqConstants
    {
        /// <summary>
        /// RabbitMQ服务地址
        /// </summary>
        public const string RabbitMqUri = "rabbitmq://localhost/";
        /// <summary>
        /// RabbitMQ服务登录用户名
        /// </summary>
        public const string UserName = "guest";
        /// <summary>
        /// RabbitMQ服务登录密码
        /// </summary>
        public const string Password = "guest";
        /// <summary>
        /// 消息队列名称
        /// </summary>
        public const string GreetingQueue = "greeting.service";
        /// <summary>
        /// 消息队列名称 有层级的消息
        /// </summary>
        public const string HierarchyMessageSubscriberQueue = "hierarchyMessage.subscriber.service";
        /// <summary>
        /// 消息队列名称 订阅模式
        /// </summary>
        public const string GreetingEventSubscriberAQueue = "greetingEvent.subscriberA.service";
        /// <summary>
        /// 消息队列名称 订阅模式
        /// </summary>
        public const string GreetingEventSubscriberBQueue = "greetingEvent.subscriberB.service";
        /// <summary>
        /// 消息队列名称 请求服务模式
        /// </summary>
        public const string RequestClientQueue = "Request.Service";
    }
}