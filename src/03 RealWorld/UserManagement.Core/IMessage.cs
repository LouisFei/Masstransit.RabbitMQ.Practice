using System;

namespace UserManagement.Core
{
    /*
     消息契约
     在MassTransit中，使用.Net 系统定义消息契约。
     消息可以使用类和接口来定义，但是，建议类型使用只读属性，不要使用行为。
     注意：强烈建议使用消息接口。MassTransit将为消息创建动态接口实现，确保消息契约与消息费者的干净分离。
    */

    /// <summary>
    /// 更新用户地址 命令消息
    /// </summary>
　　public interface UpdateCustomerAddress
    {
        /// <summary>
        /// 命令编号
        /// </summary>
        Guid CommandId { get; }
        DateTime Timestamp { get; }
        /// <summary>
        /// 用户编号
        /// </summary>
        string CustomerId { get; }
        string HouseNumber { get; }
        string Street { get; }
        string City { get; }
        string State { get; }
        string PostalCode { get; }
    }

    /// <summary>
    /// 用户地址更新　事件消息
    /// </summary>
    public interface CustomerAddressUpdated
    {
        /// <summary>
        /// 命令编号
        /// </summary>
        Guid CommandId { get; }
        DateTime Timestamp { get; }
        /// <summary>
        /// 用户编号
        /// </summary>
        string CustomerId { get; }
        string HouseNumber { get; }
        string Street { get; }
        string City { get; }
        string State { get; }
        string PostalCode { get; }
    }

    /*
        工程师在消息传递方面的一个常见错误是为消息创建基类，并尝试在消费者中分配基类——包括子类的行为。
        哎哟，这总是导致痛苦，所以对基类说“不”。
    */

    /*
        指定消息名称
        有两种主要的消息类型：事件和命令。
        当选择一个消息的名称时，消息的类型应该指定消息的时态。

        命令：
        命令告诉服务做某事。
        命令使用Send发送到端点，因为预期单个服务实例会执行命令操作。命令不应该发布。
        命令应该以动词-名词的形式来表达。
        示例命令：
            UpdateCustomerAddress
            UpgradeCustomerAccount
            SubmitOrder
        
        事件：
        事件意味着某事发生了。
        事件使用Publish，使用IBus或ConsumeContext发布。
        事件不应该直接发送到端点。
        事件应以名词+动词（过去时态）形式表示，表明发生了某事。
        示例事件：
            CustomerAddressUpdated
            CustomerAccountUpgraded
            OrderSubmitted
            OrderAccepted
            OrderRejected
            OrderShipped

        消息相关性
        由于消息通常不是孤立的，发布一条消息通常会导致发布另一条消息，然后再发布另一条消息，等等。
        跟踪这些序列是有用的，然而，要找到它们，需要有一些信息详细描述它们是如何相互关联的。

        相关性是将消息连接在一起的原理，通常是通过使用在逻辑序列的一部分中包含唯一标识符。
        在MassTransit中，唯一标识符被称为CorrelationId，它包含在消息信封中，并通过ConsumeContext或SendContext提供。
        MassTransit还包括一个会话ID，在整个相关消息集合中都是相同的。



     */


    public interface IMessage { }

    public interface IEvent : IMessage { }

    public interface ICommand : IMessage { }
}