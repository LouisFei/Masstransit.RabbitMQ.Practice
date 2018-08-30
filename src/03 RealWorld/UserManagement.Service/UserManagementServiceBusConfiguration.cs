using System;
using MassTransit;
using MassTransit.RabbitMqTransport;
using UserManagement.Core;
using UserManagement.Events;
using UserManagement.Service.Observers;

namespace UserManagement.Service
{
    /// <summary>
    /// 服务端总线配置
    /// </summary>
    public class UserManagementServiceBusConfiguration: BusConfiguration
    {
        /// <summary>
        /// 私有化类的构造函数，使类不可以在外部实例化，单例模式的前提。
        /// </summary>
        private UserManagementServiceBusConfiguration() { }

        public override string RabbitMqAddress { get; } = "rabbitmq://localhost/";
        public override string QueueName { get; } = "UserManagementServiceQueue";
        public override string RabbitMqUserName { get; } = "guest";
        public override string RabbitMqPassword { get; } = "guest";

        private static IBus _bus;

        /// <summary>
        /// 总线实例
        /// </summary>
        public static IBus BusInstance => _bus ?? (_bus = new UserManagementServiceBusConfiguration().CreateBus());

        public override Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> Configuration
        {
            get
            {
                return (cfg, host) =>
                {
                    //设置重试策略：重试３次，每次间隔时间为１分钟。
                    cfg.UseRetry(Retry.Interval(3, TimeSpan.FromMinutes(1)));

                    //设置消息处理限制：每次处理1000条，每组消息的重置间隔为1分钟。
                    cfg.UseRateLimit(1000, TimeSpan.FromSeconds(1));

                    //配置消息接收端点
                    cfg.ReceiveEndpoint(host, //接收消费的端点主机
                        QueueName, //消息队列名称
                        e => //配置消息接收处理
                        {
                            //启用Windsor容器的消息范围生命周期。
                            e.UseMessageScope();

                            //1. register consumers by manually. 手工注册消费者。
                            //e.Consumer<UserCreatedEventConsumer>();
                            //e.Consumer<UserUpdatedEventComsumer>();

                            //2. register comsumers by container. 使用IoC容器注册消费者。
                            e.LoadFrom(ApplicationBootstrapper.Container);

                            //上面的示例使用默认构造函数消费者工厂来连接消费者。下面有几个其他的消费工厂支持。
                            //使用匿名工厂方法
                            // e.Consumer(() => new YourConsumer());
                            e.Consumer(() => new UserCreatedEventConsumer());
                            //使用现有的消费者工厂
                            //e.Consumer(consumerFactory);
                            //使用基于类型的工厂，从容器中返回对象
                            //e.Consumer(consumerType, type => container.Resolve(type))
                            //使用匿名工厂方法，结合一些不错的中间件
                            e.Consumer(() => new UserCreatedEventConsumer(), x =>
                            {
                                //添加一个中间件到消费者管道中。
                                //x.UseLog(ConsoleOut, async context = > "Consumer created");
                            });

                            //连接现有的消费者实例
                            //e.Instance(existingConsumer)
                            //虽然强烈建议使用每个消息的消费者实例，但可以连接一个现有的消费者实例，每个实例都将调用该实例。
                            //消费者必须是线程安全的，因为消费方法将同时从多个线程调用。


                            //处理没有消费者的消息
                            //虽然创建消费者是首选的消息消费方式，但也有可能创建一个简单的消息处理程序。
                            //通过指定方法、匿名方法或lambda方法，消息可以在接收端点上被消耗。
                            //在这种情况下，对接收到的每个消息都调用该方法。没有创建消费者，也不执行生命周期管理。
                            e.Handler<UpdateCustomerAddress>(context =>
                            {
                                return Console.Out.WriteLineAsync($"Update customer address received: {context.Message.CustomerId}");
                            });

                            //通过IObserver观察消息
                            //一旦创建，观察者连接到接收端点，类似于消费者。
                            e.Observer(new CustomerAddressUpdatedObserver());
                        });

                    /*
                        注意：当一个消费者连接到一个接收端点时，由连接到同一个接收端点的所有消费者所消费的组合消息被*subscribed*订阅到队列中。
                        订阅方法因broker代理而异，在RabbitMQ Exchange绑定的情况下，将消息类型创建为接收端点的Exchange/ queue。
                        这些订阅是持久的，并在进程退出后保持不变。
                        这样可以确保发布或发送的消息交付到接收端点消费者之一，即使进程终止。
                        当进程启动时，队列中等待的消息将交付给消费者。
                     */
                };
            }
        }

        /// <summary>
        /// 挂载总线监视器
        /// </summary>
        public override Action<IBus> ConnectObservers
        {
            get
            {
                return bus =>
                {
                    //将一个观察者，连接到接收端点。
                    bus.ConnectReceiveObserver(new ReceiveObserver());

                    //将消息观察者连接到管道
                    bus.ConnectConsumeMessageObserver(new ConsumeObserver<UserCreatedEvent>());
                };
            }
        }
    }

    //通过IObserver观察消息
    //通过添加IObserver接口，将观察者的概念添加到.Net Framework中。MassTransit支持观察者直接连接接收端点。
    //可惜，观察者不是异步的。因此，当使用观察者时，无法对编译程序提供的异步支持进行良好的运行。
    //一个观察者使用内置的IObserver<T>接口定义。如下所示：
    public class CustomerAddressUpdatedObserver : IObserver<ConsumeContext<CustomerAddressUpdated>>
    {
        /// <summary>
        /// 通知观察者，提供程序已完成发送基于推送的通知。
        /// </summary>
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通知观察者，提供程序遇到错误情况。
        /// </summary>
        /// <param name="error">一个提供有关错误的附加信息的对象。</param>
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 向观察者提供新数据。
        /// </summary>
        /// <param name="value">当前的通知信息。</param>
        public void OnNext(ConsumeContext<CustomerAddressUpdated> context)
        {
            Console.WriteLine("Customer address was updated: {0}", context.Message.CustomerId);
        }
    }
}