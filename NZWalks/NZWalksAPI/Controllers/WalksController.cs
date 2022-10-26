using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IMapper mapper;
        public WalksController(IWalksRepository walks, IMapper map)
        {
            _walksRepository = walks;
            mapper = map;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regiondata = await _walksRepository.GetWalksAll();
            
            var regionsDTO = mapper.Map<List<Models.DTO.WalksDTO>>(regiondata);
            return Ok(regionsDTO);


        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAllById")]

        public async Task<IActionResult> GetAllById(Guid id)
        {
            var regiondata = await _walksRepository.GetWalksAsync(id);
            if (regiondata == null)
            {
                return NotFound();
            }
            var regionsDTO = mapper.Map<Models.DTO.WalksDTO>(regiondata);
            return Ok(regionsDTO);


        }

        [HttpPost]
        public async Task<IActionResult> AddWalk([FromBody] Models.DTO.AddWalkRequest walks)

        {

            //request to domain Model
            var walkdomian = new Models.Domain.Walks
            {
                Name = walks.Name,
                Length = walks.Length,

                RegionId = walks.RegionId,
                WalkDifficultyId = walks.WalkDifficultyId
            };


            //pass to the repository
            walkdomian = await _walksRepository.AddWalks(walkdomian);

            //request back to DTO
            var walkDTO = new Models.DTO.WalksDTO
            {
                Id = walkdomian.Id,
                Name = walkdomian.Name,
                Length = walkdomian.Length,

                WalkDifficultyId = walkdomian.WalkDifficultyId,
                RegionId = walkdomian.RegionId
            };

            return CreatedAtAction(nameof(GetAllById), new { id = walkDTO.Id }, walkDTO);



            //  return CreatedAtAction(nameof(GetAllWalks));



        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute]Guid id , [FromBody] Models.DTO.UpdateWalkRequest walk)
        {
            //convert DTO to Domain Models

            var updatewalk = new Models.Domain.Walks()
            {
                 Name = walk.Name,
                 Length=walk.Length,    
                 RegionId=walk.RegionId,
                 WalkDifficultyId=walk.WalkDifficultyId,
            
            };

            updatewalk = await _walksRepository.UpdateWalk(id,updatewalk);

            var updateDTO = new Models.DTO.WalksDTO()
            {
                Id=updatewalk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId

            };
            return Ok(updateDTO);



        }

        [HttpDelete]
        [Route ("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var deletewalk = await _walksRepository.DeleteWalksById(id);
            if(deletewalk == null)
            {
                return NotFound();
            }

            var WalkDTO = new Models.DTO.WalksDTO()
            {
                Name = deletewalk.Name,
                Length = deletewalk.Length,
                RegionId = deletewalk.RegionId,
                WalkDifficultyId = deletewalk.WalkDifficultyId

            };

            return Ok(deletewalk);
        }
    
    }
}
