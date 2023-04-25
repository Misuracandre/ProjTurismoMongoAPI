namespace Proj_Mongo_API.Config
{
    public interface ITurismoMongoSettings
    {
        string CustomerCollectionName { get; set; }
        string AddressCollectionName { get; set; }
        string CityCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
