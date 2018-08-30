using System;

namespace Masstransit.RabbitMQ.HierarchyMessage
{
    public interface IMessage
    {
        Guid Id { get; set; }
    }

    public class Message : IMessage
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }

    /// <summary>
    /// 用户更新消息
    /// </summary>
    public class UserUpdatedMessage : Message
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    /// 用户删除消息
    /// </summary>
    public class UserDeletedMessage : Message
    {
        public Guid Id { get; set; }
    }
}