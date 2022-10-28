using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            var regiondata = await repository.GetAll();
            var regionsDTO = mapper1.Map<List<Models.DTO.RegionDTO>>(regiondata);
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
        [HttpPost]
        public async Task<IActionResult> AddRegion(Models.DTO.AddRegionRequest rego)
        {
            if(!ValidationAddRegionRequest(rego))
            {
                return BadRequest(ModelState);
            }
            //request to domain model
            var regiondomian = new Models.Domain.Region()
            {
                Code = rego.Code,
                Area = rego.Area,
                Lat = rego.Lat,
                Long = rego.Long,
                Name = rego.Name,
                Population = rego.Population
            };

            //pass details to respository

            regiondomian = await repository.AddRegions(regiondomian);

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
            return CreatedAtAction(nameof(GetAllRegion), new { id = regionDTO.Id }, regionDTO);

        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionById([FromRoute] Guid id,[FromBody] Models.DTO.UpdateRegionRequest rego)
        {


            if (!ValidationUpdateRegionrequest(rego))
            {
                return BadRequest(ModelState);
            }
            //convert DTO to Domain Models
            var regiondomian = new Models.Domain.Region()
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
                Id = regiondomian.Id,
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

        #region private methods

        private bool ValidationAddRegionRequest(Models.DTO.AddRegionRequest rego)
        {
            if(rego == null)
            {
                ModelState.AddModelError(nameof(rego),
                    $"Add region Data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(rego.Name))
            {
                ModelState.AddModelError(nameof(rego.Name), 
                    $"{nameof(rego.Name)}cannot be null or empty or whitespace");


            }
            if (string.IsNullOrWhiteSpace(rego.Code))
            {
                ModelState.AddModelError(nameof(rego.Code), 
                    $"{nameof(rego.Code)} Cannot be null or empty or whitespace");
            }
            if(rego.Area <= 0)
            {
                ModelState.AddModelError(nameof(rego.Area),
                    $"{nameof(rego.Area)} Area cantbe less than Zero");
            }
            if(rego.Long <= 0)
            {
                ModelState.AddModelError(nameof(rego.Long),
                    $"{nameof(rego.Long)} longitute cant be less than zero");
            }

            if(rego.Lat <= 0)
            {
                ModelState.AddModelError(nameof(rego.Lat),$"{nameof(rego.Lat)} Latitude cant be less than zero");
            }
            if(rego.Population <=0)
            {
                ModelState.AddModelError(nameof(rego.Population),
                    $"{nameof(rego.Population)} Population cannot be less than zero");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }
        #endregion
        private bool ValidationUpdateRegionrequest(Models.DTO.UpdateRegionRequest urego)
        {
            if(urego == null)
            {
                ModelState.AddModelError(nameof(urego), $"{nameof(urego)} Required all fields");
            }

            if (string.IsNullOrWhiteSpace(urego.Name)) { 
            ModelState.AddModelError(nameof(urego.Name), $"{nameof(urego.Name)} Name is required");
            }
            if (string.IsNullOrWhiteSpace(urego.Code))
            {
                ModelState.AddModelError(nameof(urego.Code), $"{nameof(urego.Code)} Code is required");
            }
               if(urego.Lat <= 0) {  
             ModelState.AddModelError(nameof(urego.Lat),  $"{nameof(urego.Lat)} lat should not be less than or equal to zero");
            }
               if(urego.Long <=0) { 
            ModelState.AddModelError(nameof(urego.Long), $"{nameof(urego.Long)} Long should not be less than or equal to zero");
            }

            if (urego.Population <= 0)
            {
                ModelState.AddModelError(nameof(urego.Population), $"{nameof(urego.Population)} Population should not be less than or equal to zero");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

    }
}
