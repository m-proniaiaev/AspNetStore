namespace Store.Database.Database
{
    public class DbConfig
    {
        public string DbName { get; set; }
        public string RecordCollectionName { get; set; }
        public string CONNECTION_STRING { get; set; } = "mongodb://localhost:27017";
    }
}