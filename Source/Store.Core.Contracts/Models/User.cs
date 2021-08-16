using System;
using System.Text.Json.Serialization;
using Store.Core.Contracts.Interfaces;

namespace Store.Core.Contracts.Models
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
    }
}