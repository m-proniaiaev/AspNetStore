using System;
using System.Text.Json.Serialization;
using Store.Core.Contracts.Interfaces.Models;

namespace Store.Core.Contracts.Domain
{
    public class User : IIdentity, IAuditable, IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid Role { get; set; }
        [JsonIgnore]
        public string Hash { get; set; }
        [JsonIgnore]
        public string Salt { get; set; }
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? Edited { get; set; }
        public Guid? EditedBy { get; set; }
    }
}