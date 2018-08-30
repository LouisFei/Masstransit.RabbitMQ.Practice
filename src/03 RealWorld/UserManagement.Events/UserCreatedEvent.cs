namespace UserManagement.Events
{
    /// <summary>
    /// 用户被创建事件
    /// </summary>
    public class UserCreatedEvent : UserManagementEvent
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}