using HomeExchangeAPI.Data;
using HomeExchangeAPI.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HomeExchangeAPI.Controllers.v1
{
    //[Route("api/[controller]")]
    [Route("api/HomeExchangeAPI")]

    [ApiController]
    public class HomeExchangeAPIController : ControllerBase
    {
        private readonly ILogger<HomeExchangeAPIController> _logger;
        public HomeExchangeAPIController(ILogger<HomeExchangeAPIController> logger) {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HomeDTO>> getHomes() {
            _logger.LogInformation("Getting all homes");
            return Ok(HomeStore.homeList);
            }
        

        [HttpGet("{id:int}", Name="GetHome")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HomeDTO> GetHome(int id) {
            if (id == 0) {
                _logger.LogError("Get home error with Id" +  id);
                return BadRequest();
            }
            var home = HomeStore.homeList.FirstOrDefault(u=>u.Id == id);
            if (home == null) {
                return NotFound();
            }

            return Ok(home);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HomeDTO> CreateHome([FromBody]HomeDTO homeDTO) {

            if (HomeStore.homeList.FirstOrDefault(u => u.Name.ToLower() == homeDTO.Name.ToLower()) != null){
                 ModelState.AddModelError("customError","Home already exists!");
                 return BadRequest(ModelState);
            }
            if (homeDTO == null) {
                return BadRequest(homeDTO);
            }

            if (homeDTO.Id > 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            homeDTO.Id = HomeStore.homeList.OrderByDescending(u=>u.Id).FirstOrDefault().Id + 1;
            HomeStore.homeList.Add(homeDTO);

            return CreatedAtRoute("GetHome", new {id=homeDTO.Id},homeDTO);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
      
        [HttpDelete("{id:int}", Name = "DeleteHome")]
        public IActionResult DeleteHome(int id){
            if (id == 0) {
                return BadRequest();
            }
            var home = HomeStore.homeList.FirstOrDefault(u=>u.Id == id);
            if (home == null)
            {
                return NotFound();
            }
            HomeStore.homeList.Remove(home);
            return NoContent();
        }

        [HttpPut("{id:int}", Name="UpdateHome")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateHome(int id, [FromBody]HomeDTO homeDTO){
            if (homeDTO == null || id != homeDTO.Id){
                return BadRequest();
            }
            var home = HomeStore.homeList.FirstOrDefault(u=> u.Id == id)!;
            home.Name= homeDTO.Name;
            home.Sqft = homeDTO.Sqft;
            home.Occupancy= homeDTO.Occupancy;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name="UpdatePartialHome")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialHome(int id, JsonPatchDocument<HomeDTO> patchDTO){
            if (patchDTO == null || id == 0){
                return BadRequest();
            }
            var home = HomeStore.homeList.FirstOrDefault(u=> u.Id == id)!;
            if (home == null) {
                return BadRequest();
            }

            patchDTO.ApplyTo(home, ModelState);
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}

