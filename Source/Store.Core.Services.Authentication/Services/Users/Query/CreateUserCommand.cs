using System;

namespace Store.Core.Services.AuthHost.Services.Users.Query
{
    public class CreateUserCommand
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid[] Roles { get; set; }   
    }
}