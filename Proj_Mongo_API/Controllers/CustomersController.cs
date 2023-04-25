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

        public CustomersController(CustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> Get() => _customersService.Get();

        [HttpGet("{id.length(24)}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(string id)
        {
            var customer = _customersService.Get(id);

            if (customer == null) return NotFound();

            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            return _customersService.Create(customer);
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
