using System.Security.Cryptography.X509Certificates;
using MongoDB.Driver;
using Proj_Mongo_API.Config;
using Proj_Mongo_API.Models;

namespace Proj_Mongo_API.Services
{
    public class AddressesService
    {
        private readonly IMongoCollection<Address> _address;

        public AddressesService(ITurismoMongoSettings settings)
        {
            var address = new MongoClient(settings.ConnectionString);
            var database = address.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);
        }

        public List<Address> Get()
        {
            return _address.Find(a => true).ToList();
        }

        public Address Get(string id)
        {
            return _address.Find<Address>(a => a.Id == id).FirstOrDefault();
        }

        public Address Create(Address address)
        {
           // address.IdCity = idCity;
            _address.InsertOne(address);
            return address;
        }

        public void Update(string id, Address address)
        {
            _address.ReplaceOne(a => a.Id == address.Id, address);
        }

        public void Delete(string id) => _address.DeleteOne(a => a.Id == id);

        public void Delete(Address address)
        {
            _address.DeleteOne(a => a.Id == address.Id);
        }
    }
}
