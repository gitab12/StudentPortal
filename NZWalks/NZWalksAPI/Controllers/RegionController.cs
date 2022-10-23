using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class RegionController : Controller

    {
        private readonly IRepository repository;
        private readonly IMapper mapper1;
        public RegionController(IRepository repos, IMapper mapper)
        {
            this.repository = repos;
            this.mapper1 = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegion()
        {
            var walksdata = await repository.GetAll();
            var regionsDTO = mapper1.Map<List<Models.DTO.RegionDTO>>(walksdata);
            return Ok(regionsDTO);
        }
    }
}
