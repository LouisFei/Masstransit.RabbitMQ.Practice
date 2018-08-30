using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MassTransit;
using UserManagement.Events;
using UserManagement.Providers;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserProvider _userProvider;
        private readonly IBus _bus;

        public UsersController(IUserProvider userProvider, IBus bus)
        {
            _userProvider = userProvider;
            _bus = bus;
        }

        [HttpGet]
        [Route("api/users/createuser")]
        public string CreateUser()
        {
            //save user in local db

            _bus.Publish(new UserCreatedEvent() { UserName = "Tom", Email = "tom@google.com" });

            return "create user named Tom";
        }

        [HttpGet]
        [Route("api/users/updateUser")]
        public string UpdateUser()
        {
            _bus.Publish(new UserUpdatedEvent() { UserName = "Jim", Email = "jim@google.com" });

            return "update user named jim";
        }
    }

/*
    生产消息
    应用程序或服务可以使用两种不同的方法生产消息。
    可以使用Send发送消息，也可以使用Publish发布消息。两种方法的行为是非常不同的。

    当消息是Send时，它使用DestinationAddress传递将会到特定的端点。
    当消息是Publish时，它不会发送到特定的端点，而是广播给订阅该消息类型的任何消费者。
    对于这两个单独的行为，我们描述作为命令发送的消息，以及作为事件发布的消息。

    发送命令
    发布事件
*/

    /// <summary>
    /// 命令消息　提交订单
    /// </summary>
    public interface SubmitOrder
    {
        string OrderId { get; }
        DateTime OrderDate { get; }
        decimal OrderAmount { get; }
    }

    /// <summary>
    /// 事件消息　　订单已提交
    /// </summary>
    public interface OrderSubmitted
    {
        string OrderId { get; }
        DateTime OrderDate { get; }
    }

    public class OrderProcessor
    {
        /*
            发送命令
            将命令发送到端点，需要ISendEndpoint引用，它可以从任何发送端点提供程序（支持ISendEndpointProvider的对象）中获得。
            应用程序应该始终使用与之最接近的对象来获取发送的端点，并且每次需要它时都应该这样做——应用程序不应缓存发送端点引用。
            要从发送端提供程序获得发送端点，请使用GetSendEndpoint()方法。
         */
        /// <summary>
        /// 发送提交订单命令消息
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task SendOrder(ISendEndpoint endpoint)
        {
            await endpoint.Send<SubmitOrder>(new
            {
                OrderId = "1",
                OrderDate = DateTime.Now,
                OrderAmount = 12.34m
            }, context =>
            {
                //可以通过报头，指定故障地址
                context.FaultAddress = new Uri("rabbitmq://localhost/order_faults");
            });
        }

        /*
            发布事件
            消息的发布与消息的发送方式类似，但在这种情况下，使用单个IPublishEndpoint。
            应用相同的端点规则，应该使用发布终结点的最接近实例。
        */
        /// <summary>
        /// 发布订单被提交的事件消息
        /// </summary>
        /// <param name="publishEndpoint"></param>
        /// <returns></returns>
        public async Task NotifyOrderSubmitted(IPublishEndpoint publishEndpoint)
        {
            await publishEndpoint.Publish<OrderSubmitted>(new
            {
                OrderId = "27",
                OrderDate = DateTime.Now
            });
        }


    }
}




