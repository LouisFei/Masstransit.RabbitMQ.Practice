using System;
using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.HelloWorld.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //连接工厂
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection()) //创建连接
            {
                using (var channel = connection.CreateModel()) //创建信道
                {
                    //创建一个名为"hello"的队列，防止producer端没有创建该队列
                    channel.QueueDeclare(queue: "hello", //消息队列名称
                                         durable: false, //消息队列非持久化
                                         exclusive: false, //消息队列非排它
                                         autoDelete: false, //消息队列非自动删除
                                         arguments: null);

                    //回调，当consumer收到消息后会执行该函数
                    var consumer = new EventingBasicConsumer(channel); //消息消费者

                    //消息消费者接收到消息的事件处理方法
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };

                    //消费队列"hell"中的消息
                    channel.BasicConsume(queue: "hello",
                                         noAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
