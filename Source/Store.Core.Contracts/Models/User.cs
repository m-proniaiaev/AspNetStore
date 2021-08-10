using System;
using Store.Core.Contracts.Interfaces;

namespace Store.Core.Contracts.Models
{
    public class User : IIdentity, IAuditable, IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid[] Roles { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
    }
}