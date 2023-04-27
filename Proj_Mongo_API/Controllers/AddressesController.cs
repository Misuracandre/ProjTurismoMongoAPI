using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Proj_Mongo_API.Models;
using Proj_Mongo_API.Services;

namespace Proj_Mongo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AddressesService _addressesServices;
        private readonly CitiesService _citiesService;

        public AddressesController(AddressesService services, CitiesService citiesService)
        {
            _addressesServices = services;
            _citiesService = citiesService;
        }

        [HttpGet]
        public ActionResult<List<Address>> Get()
        {
            var addresses = _addressesServices.Get();

            if (addresses == null) return NotFound();

            foreach (var address in addresses)
            {
                var city = _citiesService.Get(address.IdCity.ToString());

                if (city != null)
                {
                    address.IdCity = city;
                }
            }
            return addresses;
        }

        [HttpGet("{id:length(24)}", Name = "GetAdress")]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressesServices.Get(id);

            if (address == null) return NotFound();

            var city = _citiesService.Get(address.IdCity.ToString());

            if (city != null)
            {
                address.IdCity = city;
            }
            return address;
        }

        [HttpPost]
        public ActionResult<Address> Create(Address address)
        {
            if (address == null) return NotFound();

            var city = _citiesService.Get(address.IdCity.Id);

            if (city == null)
            {
                return NotFound();
            }
            address.IdCity = city;

            var createdAddress = _addressesServices.Create(address);

            if (createdAddress != null)
            {
                return createdAddress;
            }

            return NotFound();
        }

        [HttpPut("{id:length(24)}")]

        public ActionResult Update(string id, Address address)
        {
            var addressToUpdate = _addressesServices.Get(id);

            if (addressToUpdate == null) return NotFound();

            addressToUpdate.Street = address.Street;
            addressToUpdate.Number = address.Number;
            addressToUpdate.ZipCode = address.ZipCode;
            addressToUpdate.IdCity = address.IdCity;

            var city = _citiesService.Get(address.IdCity.ToString());

            if (city == null)
            {
                _citiesService.Create(address.IdCity);
            }
            city.Description = address.IdCity.Description;

            addressToUpdate.IdCity = city;

            _addressesServices.Update(id, addressToUpdate);
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
