using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Store.Core.Interfaces;

namespace Store.Core.Models
{
    public class Record : IEntity, IAuditable, ISellable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public DateTime? SoldDate { get; set; }
    }
}