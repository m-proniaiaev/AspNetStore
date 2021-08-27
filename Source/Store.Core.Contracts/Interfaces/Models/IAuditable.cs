using System;

namespace Store.Core.Contracts.Interfaces.Models
{
    public interface IAuditable
    {
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? Edited { get; set; }
        public Guid? EditedBy { get; set; }
    }
}