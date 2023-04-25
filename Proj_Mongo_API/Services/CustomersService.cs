using MongoDB.Driver;
using Proj_Mongo_API.Config;
using Proj_Mongo_API.Models;

namespace Proj_Mongo_API.Services
{
    public class CustomersService
    {
        private readonly IMongoCollection<Customer> _customer;

        public CustomersService(ITurismoMongoSettings settings)
        {
            var customer = new MongoClient(settings.ConnectionString);
            var database = customer.GetDatabase(settings.DatabaseName);
            _customer = database.GetCollection<Customer>(settings.CustomerCollectionName);
        }

        public List<Customer> Get()
        {
            return _customer.Find(c => true).ToList();
        }

        public Customer Get(string id)
        {
            return _customer.Find<Customer>(c => c.Id == id).FirstOrDefault();
        }

        public Customer Create(Customer customer)
        {
            _customer.InsertOne(customer);
            return customer;
        }

        public void Update(string id, Customer customer)
        {
            _customer.ReplaceOne(c => c.Id == customer.Id, customer);
        }

        public void Delete(string id)
        {
            _customer.DeleteOne(c => c.Id == id);
        }

        public void Delete(Customer customer)
        {
            _customer.DeleteOne(c => c.Id == customer.Id);
        }
    }
}
