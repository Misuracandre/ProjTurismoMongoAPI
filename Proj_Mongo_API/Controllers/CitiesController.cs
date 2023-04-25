using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proj_Mongo_API.Models;
using Proj_Mongo_API.Services;

namespace Proj_Mongo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesService _citiesService;

        public CitiesController(CitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        [HttpGet]

        public ActionResult<List<City>> Get() => _citiesService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCity")]

        public ActionResult<City> Get(string id)
        {
            var city = _citiesService.Get(id);

            if (city == null) return NotFound();

            return city;
        }

        [HttpPost]

        public ActionResult<City> Create(City city)
        {
            return _citiesService.Create(city);
        }

        [HttpPut("{id:length(24)}")]

        public ActionResult Update(string id, City city)
        {
            var c = _citiesService.Get(id);

            if (c == null) return NotFound();
            _citiesService.Update(id, city);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]

        public ActionResult Delete(string id)
        {
            if(id == null) return NotFound();

            var city = _citiesService.Get(id);
            if (city == null) return NotFound();

            _citiesService.Delete(id);
            return Ok();
        }
    }
}
