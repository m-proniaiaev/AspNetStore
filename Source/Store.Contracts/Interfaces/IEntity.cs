using System;

namespace Store.Contracts.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}