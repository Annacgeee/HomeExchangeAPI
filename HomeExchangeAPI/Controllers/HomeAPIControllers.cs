using System.Net;
using AutoMapper;
using HomeExchangeAPI.Models;
using HomeExchangeAPI.Models.Dto;
using HomeExchangeAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace HomeExchangeAPI.Controllers.v1
{
    //[Route("api/[controller]")]
    [Route("api/HomeExchangeAPI")]

    [ApiController]
    public class HomeExchangeAPIController : ControllerBase
    {
       
        protected APIResponse _response;
          private readonly IHomeRepository _dbHome;
          private readonly IMapper _mapper;

        public HomeExchangeAPIController(IHomeRepository db, IMapper mapper) {
            _dbHome = db;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> getHomes() {
            try {
            IEnumerable<Home> homeList = await _dbHome.GetAllAsync();
            _response.Result=_mapper.Map<List<HomeDTO>>(homeList);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){ex.ToString()};
            }

            return _response;

            }
        
        [HttpGet("{id:int}", Name="GetHome")]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHome(int id) {

            try{

            
            if (id == 0) {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var home = await _dbHome.GetAsync(u => u.Id == id);
            if (home == null) {
                return NotFound();
            }

            _response.Result = _mapper.Map<HomeDTO>(home);
            _response.StatusCode = HttpStatusCode.OK;
           

            return Ok(_response);
             }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){ex.ToString()};
            }

            return _response;
        }

        [HttpPost]
        [Authorize(Role="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateHome([FromBody]HomeCreateDTO createDTO) {
            try {
            if (await _dbHome.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null){
                 ModelState.AddModelError("customError","Home already exists!");
                 return BadRequest(ModelState);
            }
            if (createDTO == null) {
                return BadRequest(createDTO);
            }

            // if (homeDTO.Id > 0) {
            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }

            Home home = _mapper.Map<Home>(createDTO);
            // Home model = new Home(){
            //     Amenity = createDTO.Amenity,
            //     Details = createDTO.Details,
            
            //     ImageUrl= createDTO.ImageUrl,
            //     Name=createDTO.Name,
            //     Occupancy=createDTO.Occupancy,
            //     Rate=createDTO.Rate,
            //     Sqft= createDTO.Sqft
            // };
           
            await _dbHome.CreateAsync(home);
             _response.Result = _mapper.Map<HomeDTO>(home);
            _response.StatusCode = HttpStatusCode.Created;
           
            return CreatedAtRoute("GetHome", new {id=home.Id},_response);
             }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){ex.ToString()};
            }

            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteHome")]
        [Authorize(Role="admin")]
        public async Task<ActionResult<APIResponse>> DeleteHome(int id){
            try {
            if (id == 0) {
                return BadRequest();
            }
            var home = await _dbHome.GetAsync(u=>u.Id == id);
            if (home == null)
            {
                return NotFound();
            }
            await _dbHome.RemoveAsync(home);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            
            return Ok(_response);
             }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){ex.ToString()};
            }

            return _response;
        }

        [HttpPut("{id:int}", Name="UpdateHome")]
        [Authorize(Role="admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateHome(int id, [FromBody]HomeUpdateDTO updateDTO){
            try {
            if (updateDTO == null || id != updateDTO.Id){
                return BadRequest();
            }
            
            Home model = _mapper.Map<Home>(updateDTO);

            await _dbHome.UpdateAsync(model);
    
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
           
            return Ok(_response);
             }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){ex.ToString()};
            }

            return _response;
        }

        [HttpPatch("{id:int}", Name="UpdatePartialHome")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialHome(int id, JsonPatchDocument<HomeUpdateDTO> patchDTO){
            if (patchDTO == null || id == 0){
                return BadRequest();
            }
            var home = await _dbHome.GetAsync(u=> u.Id == id,tracked:false);

            HomeUpdateDTO homeDTO = _mapper.Map<HomeUpdateDTO>(home);
            


            if (home == null) {
                return BadRequest();
            }

            patchDTO.ApplyTo(homeDTO, ModelState);
            
            Home model = _mapper.Map<Home>(homeDTO);
            
            await _dbHome.UpdateAsync(model);
           
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}

