namespace Proj_Mongo_API.Config
{
    public class TurismoMongoSettings : ITurismoMongoSettings
    {
        public string CustomerCollectionName { get; set; }
        public string AddressCollectionName { get; set; }
        public string CityCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
