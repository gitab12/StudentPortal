using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models;
using NZWalksAPI.Models.DTO;
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

        [HttpGet]
        //id:guid will restrict to take other values except guid id
        [Route("{id:guid}")]
        [ActionName("GetAllRegion")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var walksdataid = await repository.GetAsync(id);
            if (walksdataid == null)
            {
                return NotFound();
            }
            var regionDtoid = mapper1.Map<Models.DTO.RegionDTO>(walksdataid);
            return Ok(regionDtoid);

        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionById([FromRoute] Guid id,[FromBody] Models.DTO.UpdateRegionRequest rego)
        {

            //convert DTO to Domain Models
            var regiondomian = new Models.Region()
            {
                Code = rego.Code,
                Area = rego.Area,
                Lat = rego.Lat,
                Long = rego.Long,
                Name = rego.Name,
                Population = rego.Population
            };

             regiondomian = await repository.UpdateWalk(id,regiondomian);
            if(regiondomian == null)
            { 
                return NotFound();
            
            }
            var regionDTO = new Models.DTO.RegionDTO()
            {
                Id= regiondomian.Id,
                Code = regiondomian.Code,
                Area = regiondomian.Area,
                Lat = regiondomian.Lat,
                Long = regiondomian.Long,
                Name = regiondomian.Name,
                Population = regiondomian.Population

            };

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionById(Guid id)
        {
            var walksdatadelete = await repository.DeleteById(id);
            if (walksdatadelete == null)
            {
                return NotFound();
            }

            var regionDTO = new Models.DTO.RegionDTO()
            {
                Code = walksdatadelete.Code,
                Area = walksdatadelete.Area,
                Lat = walksdatadelete.Lat,
                Long = walksdatadelete.Long,
                Name = walksdatadelete.Name,
                Population = walksdatadelete.Population

            };
            //  var regionDtoid = mapper1.Map<Models.DTO.RegionDTO>(walksdatadelete);
            return Ok(regionDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(Models.DTO.AddRegionRequest rego)
        {
            //request to domain model
            var regiondomian = new Models.Region()
            {
                Code = rego.Code,
                Area = rego.Area,
                Lat = rego.Lat,
                Long = rego.Long,
                Name = rego.Name,
                Population = rego.Population
            };

            //pass details to respository

          regiondomian = await  repository.AddRegions(regiondomian);

            //convert back to DTO
            var regionDTO = new Models.DTO.RegionDTO()
            {
                Code = regiondomian.Code,
                Area = regiondomian.Area,
                Lat = regiondomian.Lat,
                Long = regiondomian.Long,
                Name = regiondomian.Name,
                Population = regiondomian.Population

            };
            //GetAllRegion is a ActionName which is defined as a attribuite in Getall method
            return CreatedAtAction(nameof(GetAllRegion),new { id= regionDTO.Id}, regionDTO);
            
        }
    }
}
