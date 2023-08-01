using AutoMapper;
using HomeExchangeAPI.Data;
using HomeExchangeAPI.Models;
using HomeExchangeAPI.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace HomeExchangeAPI.Controllers.v1
{
    //[Route("api/[controller]")]
    [Route("api/HomeExchangeAPI")]

    [ApiController]
    public class HomeExchangeAPIController : ControllerBase
    {
       
        
          private readonly ApplicationDbContext _db;
          private readonly IMapper _mapper;

        public HomeExchangeAPIController(ApplicationDbContext db, IMapper mapper) {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HomeDTO>>> getHomes() {
            IEnumerable<Home> homeList = await _db.Homes.ToListAsync();
            
            return Ok(_mapper.Map<List<HomeDTO>>(homeList));
            }
        
        [HttpGet("{id:int}", Name="GetHome")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HomeDTO>> GetHome(int id) {
            if (id == 0) {
                
                return BadRequest();
            }
            var home = await _db.Homes.FirstOrDefaultAsync(u=>u.Id == id);
            if (home == null) {
                return NotFound();
            }

            return Ok(_mapper.Map<HomeDTO>(home));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HomeDTO>> CreateHome([FromBody]HomeCreateDTO createDTO) {

            if (await _db.Homes.FirstOrDefaultAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null){
                 ModelState.AddModelError("customError","Home already exists!");
                 return BadRequest(ModelState);
            }
            if (createDTO == null) {
                return BadRequest(createDTO);
            }

            // if (homeDTO.Id > 0) {
            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }

            Home model = _mapper.Map<Home>(createDTO);
            // Home model = new Home(){
            //     Amenity = createDTO.Amenity,
            //     Details = createDTO.Details,
            
            //     ImageUrl= createDTO.ImageUrl,
            //     Name=createDTO.Name,
            //     Occupancy=createDTO.Occupancy,
            //     Rate=createDTO.Rate,
            //     Sqft= createDTO.Sqft
            // };
            await _db.Homes.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetHome", new {id=model.Id},model);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
      
        [HttpDelete("{id:int}", Name = "DeleteHome")]
        public async Task<IActionResult> DeleteHome(int id){
            if (id == 0) {
                return BadRequest();
            }
            var home = await _db.Homes.FirstOrDefaultAsync(u=>u.Id == id);
            if (home == null)
            {
                return NotFound();
            }
            _db.Homes.Remove(home);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name="UpdateHome")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateHome(int id, [FromBody]HomeUpdateDTO updateDTO){
            if (updateDTO == null || id != updateDTO.Id){
                return BadRequest();
            }
            
            Home model = _mapper.Map<Home>(updateDTO);

            _db.Homes.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name="UpdatePartialHome")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialHome(int id, JsonPatchDocument<HomeUpdateDTO> patchDTO){
            if (patchDTO == null || id == 0){
                return BadRequest();
            }
            var home = await _db.Homes.AsNoTracking().FirstOrDefaultAsync(u=> u.Id == id)!;

            HomeUpdateDTO homeDTO = _mapper.Map<HomeUpdateDTO>(home);
            


            if (home == null) {
                return BadRequest();
            }

            patchDTO.ApplyTo(homeDTO, ModelState);
            
            Home model = _mapper.Map<Home>(homeDTO);
            
            _db.Homes.Update(model);
            await _db.SaveChangesAsync();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}

