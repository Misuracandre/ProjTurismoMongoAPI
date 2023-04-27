using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proj_Mongo_API.Models;
using Proj_Mongo_API.Services;

namespace Proj_Mongo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersService _customersService;
        private readonly AddressesService _addressesService;
        private readonly CitiesService _citiesService;

        public CustomersController(CustomersService customersService, AddressesService addressesService, CitiesService citiesService)
        {
            _customersService = customersService;
            _addressesService = addressesService;
            _citiesService = citiesService;

        }

        [HttpGet]
        public ActionResult<List<Customer>> Get()
        {
            var customers = _customersService.Get();

            if (customers == null) return NotFound();

            foreach (var customer in customers)
            {
                var address = _addressesService.Get(customer.IdAddress.ToString());

                if (address != null)
                {
                    var city = _citiesService.Get(address.IdCity.ToString());

                    if (city != null)
                    {
                        address.IdCity = city;
                        customer.IdAddress = address;
                    }
                }
            }
            return customers;
        }

        [HttpGet("{id.length(24)}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(string id)
        {
            var customer = _customersService.Get(id);
            if (customer == null) return NotFound();

            var address = _addressesService.Get(customer.IdAddress.ToString());
            if (address == null) return NotFound();

            var city = _citiesService.Get(address.IdCity.ToString());
            if (city == null) return NotFound();

            address.IdCity = city;
            customer.IdAddress = address;

            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            if (customer == null) return NotFound();

            if (customer.IdAddress != null)
            {
                var existingAddress = _addressesService.Get(customer.IdAddress.Id);
                if (existingAddress == null)
                {
                    return NotFound();
                }

                if (existingAddress.IdCity == null && customer.IdAddress.IdCity != null)
                {
                    var existingCity = _citiesService.Get(customer.IdAddress.IdCity.Id);
                    if (existingCity == null)
                    {
                        existingCity = new City
                        {
                            Description = customer.IdAddress.IdCity.Description,
                        };
                        _citiesService.Create(existingCity);
                    }
                    existingAddress.IdCity = existingCity;
                }
                customer.IdAddress = existingAddress;
            }
            else
            {
                var newAddress = new Address
                {
                    Street = customer.IdAddress.Street,
                    Number = customer.IdAddress.Number,
                    Neighborhood = customer.IdAddress.Neighborhood,
                    ZipCode = customer.IdAddress.ZipCode,
                };

                if (customer.IdAddress.IdCity != null)
                {
                    var existingCity = _citiesService.Get(customer.IdAddress.IdCity.Id);
                    if (existingCity == null)
                    {
                        existingCity = new City
                        {
                            Description = customer.IdAddress.IdCity.Description,
                        };
                        _citiesService.Create(existingCity);
                    }
                    newAddress.IdCity = existingCity;
                }

                _addressesService.Create(newAddress);
                customer.IdAddress = newAddress;
            }

            var createdCustomer = _customersService.Create(customer);

            if (createdCustomer != null)
            {
                return createdCustomer;
            }
            return NotFound();
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult Update(string id, Customer customer)
        {
            var customerToUpdate = _customersService.Get(id);

            if (customerToUpdate == null) return NotFound();

            customerToUpdate.Name = customer.Name;
            customerToUpdate.Phone = customer.Phone;
            customerToUpdate.IdAddress = customer.IdAddress;

            var address = _addressesService.Get(customer.IdAddress.ToString());
            if (address == null)
            {
                address = _addressesService.Create(customer.IdAddress);
            }

            address.Street = customer.IdAddress.Street;
            address.Number = customer.IdAddress.Number;
            address.Neighborhood = customer.IdAddress.Neighborhood;
            address.IdCity = customer.IdAddress.IdCity;

            var city = _citiesService.Get(address.IdCity.ToString());
            if (city == null)
            {
                city = _citiesService.Create(address.IdCity);
            }

            city.Description = address.IdCity.Description;

            address.IdCity = city;

            customerToUpdate.IdAddress = address;

            _customersService.Update(id, customerToUpdate);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult DeleteCustomer(string id)
        {
            if (id == null) return NotFound();

            var customer = _customersService.Get(id);
            if (customer == null) return NotFound();

            _customersService.Delete(id);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult DeleteAddress(string id)
        {
            if (id == null) return NotFound();

            var customer = _customersService.Get(id);
            if (customer == null) return NotFound();

            customer.IdAddress = null;
            _customersService.Update(id, customer);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult DeleteCity(string id)
        {
            if (id == null) return NotFound();

            var customer = _customersService.Get(id);
            if (customer == null) return NotFound();

            var addressId = customer.IdAddress.ToString();
            var address = _addressesService.Get(addressId);
            if (address != null)
            {
                address.IdCity = null;
            }

            _customersService.Update(id, customer);

            return Ok();
        }
    }
}
