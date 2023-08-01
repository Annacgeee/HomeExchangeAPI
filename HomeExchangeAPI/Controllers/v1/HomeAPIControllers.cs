using HomeExchangeAPI.Data;
using HomeExchangeAPI.Models;
using HomeExchangeAPI.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeExchangeAPI.Controllers.v1
{
    //[Route("api/[controller]")]
    [Route("api/HomeExchangeAPI")]

    [ApiController]
    public class HomeExchangeAPIController : ControllerBase
    {
       
        
          private readonly ApplicationDbContext _db;

        public HomeExchangeAPIController(ApplicationDbContext db) {
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HomeDTO>> getHomes() {
           
            return Ok(_db.Homes.ToList());
            }
        
      

        [HttpGet("{id:int}", Name="GetHome")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HomeDTO> GetHome(int id) {
            if (id == 0) {
                
                return BadRequest();
            }
            var home = _db.Homes.FirstOrDefault(u=>u.Id == id);
            if (home == null) {
                return NotFound();
            }

            return Ok(home);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HomeDTO> CreateHome([FromBody]HomeCreateDTO homeDTO) {

            if (_db.Homes.FirstOrDefault(u => u.Name.ToLower() == homeDTO.Name.ToLower()) != null){
                 ModelState.AddModelError("customError","Home already exists!");
                 return BadRequest(ModelState);
            }
            if (homeDTO == null) {
                return BadRequest(homeDTO);
            }

            // if (homeDTO.Id > 0) {
            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }
            Home model = new Home(){
                Amenity = homeDTO.Amenity,
                Details = homeDTO.Details,
            
                ImageUrl= homeDTO.ImageUrl,
                Name=homeDTO.Name,
                Occupancy=homeDTO.Occupancy,
                Rate=homeDTO.Rate,
                Sqft= homeDTO.Sqft
            };
            _db.Homes.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetHome", new {id=model.Id},model);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
      
        [HttpDelete("{id:int}", Name = "DeleteHome")]
        public IActionResult DeleteHome(int id){
            if (id == 0) {
                return BadRequest();
            }
            var home = _db.Homes.FirstOrDefault(u=>u.Id == id);
            if (home == null)
            {
                return NotFound();
            }
            _db.Homes.Remove(home);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name="UpdateHome")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateHome(int id, [FromBody]HomeUpdateDTO homeDTO){
            if (homeDTO == null || id != homeDTO.Id){
                return BadRequest();
            }
            // var home = HomeStore.homeList.FirstOrDefault(u=> u.Id == id)!;
            // home.Name= homeDTO.Name;
            // home.Sqft = homeDTO.Sqft;
            // home.Occupancy= homeDTO.Occupancy;
Home model = new Home(){
                Amenity = homeDTO.Amenity,
                Details = homeDTO.Details,
                Id= homeDTO.Id,
                ImageUrl= homeDTO.ImageUrl,
                Name=homeDTO.Name,
                Occupancy=homeDTO.Occupancy,
                Rate=homeDTO.Rate,
                Sqft= homeDTO.Sqft
            };
            _db.Homes.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name="UpdatePartialHome")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialHome(int id, JsonPatchDocument<HomeUpdateDTO> patchDTO){
            if (patchDTO == null || id == 0){
                return BadRequest();
            }
            var home = _db.Homes.AsNoTracking().FirstOrDefault(u=> u.Id == id)!;
            

            HomeUpdateDTO homeDTO = new() {
                Amenity = home.Amenity,
                Details = home.Details,
                Id= home.Id,
                ImageUrl= home.ImageUrl,
                Name=home.Name,
                Occupancy=home.Occupancy,
                Rate=home.Rate,
                Sqft= home.Sqft
            };

            if (home == null) {
                return BadRequest();
            }

            patchDTO.ApplyTo(homeDTO, ModelState);

            Home model = new() {
                Amenity = homeDTO.Amenity,
                Details = homeDTO.Details,
                Id= homeDTO.Id,
                ImageUrl= homeDTO.ImageUrl,
                Name=homeDTO.Name,
                Occupancy=homeDTO.Occupancy,
                Rate=homeDTO.Rate,
                Sqft= homeDTO.Sqft
            };

            _db.Homes.Update(model);
            _db.SaveChanges();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}

