namespace Store.Core.Database.Database
{
    public class DbConfig
    {
        public string DbName { get; set; }
        public string RecordCollectionName { get; set; }
        public string SellerCollectionName { get; set; }
        public string UserCollectionName { get; set; }
        public string RolesCollectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}