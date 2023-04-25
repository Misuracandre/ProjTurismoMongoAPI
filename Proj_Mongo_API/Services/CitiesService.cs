using Proj_Mongo_API.Config;
using Proj_Mongo_API.Models;
using MongoDB.Driver;

namespace Proj_Mongo_API.Services
{
    public class CitiesService
    {
        private readonly IMongoCollection<City> _city;

        public CitiesService(ITurismoMongoSettings settings)
        {
            var city = new MongoClient(settings.ConnectionString);
            var database = city.GetDatabase(settings.DatabaseName);
            _city = database.GetCollection<City>(settings.CityCollectionName);
        }

        public List<City> Get() => _city.Find(c => true).ToList();

        public City Get(string id) => _city.Find(c => c.Id == id).FirstOrDefault();

        public City Create(City city)
        {
            _city.InsertOne(city);
            return city;
        }

        public void Update(string id, City city)
        {
            _city.ReplaceOne(c => c.Id == city.Id, city);
        }

        public void Delete(string id) => _city.DeleteOne(c => c.Id == id);

        public void Delete(City city)
        {
            _city.DeleteOne(c => c.Id == city.Id);
        }
        
    }


}
