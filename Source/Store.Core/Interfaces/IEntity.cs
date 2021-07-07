using System;

namespace Store.Core.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}