using System;
using System.Collections.Generic;
using Store.Core.Interfaces;
using Store.Core.Models;

namespace Store.Core.Services
{
    public class RecordService : IRecordService
    {
        public IEnumerable<Record> GetRecords()
        {
            return new List<Record>()
            {
                new Record
                {
                    Id = new Guid(),
                    Name = "Asus",
                    Price = 1700m,
                    IsSold = false
                }
            };
        }
    }
}