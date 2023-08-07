using System.Net;
using AutoMapper;
using HomeExchangeAPI.Models;
using HomeExchangeAPI.Models.Dto;
using HomeExchangeAPI.Repository.IRepository;
// using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;



namespace HomeExchangeAPI.Controllers.v1
{
    [Route("api/HomeNumberAPI")]
    [ApiController]
    // [ApiVersion("1.0")]

    public class HomeNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IHomeNumberRepository _dbHomeNumber;
        private readonly IHomeRepository _dbHome;
        private readonly IMapper _mapper;
        public HomeNumberAPIController(IHomeNumberRepository dbHomeNumber, IMapper mapper,
            IHomeRepository dbHome)
        {
            _dbHomeNumber = dbHomeNumber;
            _mapper = mapper;
            _response = new();
            _dbHome = dbHome;
        }


        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "String1", "string2" };
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetHomeNumbers()
        {
            try
            {

                // IEnumerable<HomeNumber> HomeNumberList = await _dbHomeNumber.GetAllAsync(includeProperties: "Home");
                IEnumerable<Home> HomeNumberList = await _dbHome.GetAllAsync();
                _response.Result = _mapper.Map<List<HomeNumberDTO>>(HomeNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }


        [HttpGet("{id:int}", Name = "GetHomeNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHomeNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var HomeNumber = await _dbHomeNumber.GetAsync(u => u.HomeNo == id);
                if (HomeNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<HomeNumberDTO>(HomeNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateHomeNumber([FromBody] HomeNumberCreateDTO createDTO)
        {
            try
            {

                if (await _dbHomeNumber.GetAsync(u => u.HomeNo == createDTO.HomeNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Home Number already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _dbHome.GetAsync(u => u.Id == createDTO.HomeID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Home ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                HomeNumber HomeNumber = _mapper.Map<HomeNumber>(createDTO);


                await _dbHomeNumber.CreateAsync(HomeNumber);
                _response.Result = _mapper.Map<HomeNumberDTO>(HomeNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetHome", new { id = HomeNumber.HomeNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteHomeNumber")]
        public async Task<ActionResult<APIResponse>> DeleteHomeNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var HomeNumber = await _dbHomeNumber.GetAsync(u => u.HomeNo == id);
                if (HomeNumber == null)
                {
                    return NotFound();
                }
                await _dbHomeNumber.RemoveAsync(HomeNumber);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        // [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateHomeNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateHomeNumber(int id, [FromBody] HomeNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.HomeNo)
                {
                    return BadRequest();
                }
                if (await _dbHome.GetAsync(u => u.Id == updateDTO.HomeID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Home ID is Invalid!");
                    return BadRequest(ModelState);
                }
                HomeNumber model = _mapper.Map<HomeNumber>(updateDTO);

                await _dbHomeNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    }
}
