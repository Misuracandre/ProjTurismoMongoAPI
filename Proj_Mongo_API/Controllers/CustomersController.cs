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
        public ActionResult<List<Customer>> Get() => _customersService.Get();

        [HttpGet("{id.length(24)}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(string id)
        {
            var customer = _customersService.Get(id);

            if (customer == null) return NotFound();

            var address = _addressesService.Get(customer.IdAddress.ToString());
            if (address == null) return NotFound();

            customer.IdAddress = address;

            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            var address = _addressesService.Get(customer.IdAddress.Id.ToString());

            if (address == null)
            {
                // Certifica-se de que o ID da cidade seja válido
                var city = _citiesService.Get(customer.IdAddress.IdCity.ToString());
                if (city == null)
                {
                    return BadRequest("Invalid city ID");
                }

                address = new Address
                {
                    Street = customer.IdAddress.Street,
                    Number = customer.IdAddress.Number,
                    Neighborhood = customer.IdAddress.Neighborhood,
                    ZipCode = customer.IdAddress.ZipCode,
                    IdCity = customer.IdAddress.IdCity,
                };

                var createdAddress = _addressesService.Create(address);
                if (createdAddress != null)
                {
                    // Atribui o novo endereço criado à propriedade customer.IdAddress
                    customer.IdAddress = createdAddress;

                    var createdCustomer = _customersService.Create(customer);
                    if (createdCustomer != null)
                    {
                        return createdCustomer;
                    }
                }
            }
            return NotFound();
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult Update(string id, Customer customer)
        {
            var c = _customersService.Get(id);

            if (c == null) return NotFound();

            _customersService.Update(id, customer);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            if (id == null) return NotFound();

            var customer = _customersService.Get(id);
            if (customer == null) return NotFound();

            _customersService.Delete(id);
            return Ok();
        }
    }
}
