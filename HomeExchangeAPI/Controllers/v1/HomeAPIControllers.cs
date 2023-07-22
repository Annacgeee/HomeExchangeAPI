using HomeExchangeAPI.Models;
using HomeExchangeAPI.Data;
using HomeExchangeAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HomeExchangeAPI.Controllers.v1
{
    //[Route("api/[controller]")]
    [Route("api/HomeExchangeAPI")]

    [ApiController]
    public class HomeExchangeAPI : ControllerBase
    {
        [HttpGet]

        public IEnumerable<HomeDTO> getHomes() {
            return HomeStore.homeList;
            }
        }
    }

