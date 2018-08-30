using System;
using UserManagement.Core;

namespace UserManagement.Events
{
    public class UserManagementEvent : IEvent
    {
        public UserManagementEvent()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}