using System;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Records.Queries
{
    public class RecordCacheHelper
    {
        public static Record MarkRecordAsSold(Record record)
        {
            var cacheRecord = record;
            cacheRecord.IsSold = true;
            cacheRecord.SoldDate = DateTime.Now;
            return cacheRecord;
        }
    }
}