using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Data;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficulty walkDifficulty;
        private readonly IMapper mapper1;
        public WalkDifficultyController(IWalkDifficulty walkd, IMapper mapper)
        {
            walkDifficulty = walkd;
            mapper1 = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walkde = await walkDifficulty.GetAll();
            var walkDTO = mapper1.Map<List<Models.DTO.WalkDifficultyDTO>>(walkde);
            return Ok(walkDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("AddWalkdifficulty")]
        public async Task<IActionResult> GetAllbyid(Guid id)
        {
            var walkde = await walkDifficulty.GetAsync(id);
            if(walkde == null)
            {
                return null;
            }
           var walkDTO = mapper1.Map<Models.DTO.WalkDifficultyDTO>(walkde);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkdifficulty(Models.DTO.AddDificultyRequest walkdif)
        {
            var walkdiff = new Models.Domain.WalkDifficulty
            {
                Code = walkdif.Code
            };
            walkdiff = await walkDifficulty.AddWalkDifficulty(walkdiff);

            var walkdiffDTO = mapper1.Map<Models.DTO.WalkDifficultyDTO>(walkdiff);
            return CreatedAtAction(nameof(AddWalkdifficulty), new { id = walkdiffDTO.Id},walkdiffDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walkid = await walkDifficulty.DeleteById(id);
            if(walkid == null)
            {
                return null;
            }
            var walkdiff = mapper1.Map<Models.DTO.WalkDifficultyDTO>(walkid);
            return Ok(walkdiff);
        }

        [HttpPut]
        [Route("{id:guid}")]
        
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id,
           Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // Validate the incoming request
            //if (!ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            // Convert DTO to Domiain Model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };

            // Call repository to update
            walkDifficultyDomain = await walkDifficulty.UpdateWalk(id, walkDifficultyDomain);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert Domain to DTO
            var walkDifficultyDTO = mapper1.Map<Models.DTO.WalkDifficultyDTO>(walkDifficultyDomain);

            // Return response
            return Ok(walkDifficultyDTO);
        }



    }
}
