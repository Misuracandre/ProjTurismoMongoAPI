using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proj_Mongo_API.Models;
using Proj_Mongo_API.Services;

namespace Proj_Mongo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AddressesService _addressesServices;

        public AddressesController(AddressesService services)
        {
            _addressesServices = services;
        }

        [HttpGet]
        public ActionResult<List<Address>> Get()
        {
            return _addressesServices.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetAdress")]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressesServices.Get(id);

            if (address == null) return NotFound();

            return address;
        }

        [HttpPost]
        public ActionResult<Address> Create(Address address)
        {
            return _addressesServices.Create(address);
        }

        [HttpPut("{id:length(24)}")]

        public ActionResult Update(string id, Address address)
        {
            var c = _addressesServices.Get(id);

            if (c == null) return NotFound();

            _addressesServices.Update(id, address);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            if (id == null) return NotFound();

            var address = _addressesServices.Get(id);
            if (address == null) return NotFound();

            _addressesServices.Delete(id);
            return Ok();
        }
    }
}
