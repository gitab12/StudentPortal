using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Text.RegularExpressions;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IMapper mapper;
        private readonly IRepository repo1;
        private readonly IWalkDifficulty WalkDifficulty;
        public WalksController(IWalksRepository walks, IMapper map, IRepository repo,IWalkDifficulty dif)
        {
            _walksRepository = walks;
            mapper = map;
            repo1 = repo;
            WalkDifficulty = dif;
            
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
            //validation

         if(!(await ValidationAddWalkRequest(walks)))
            {
                return BadRequest(ModelState);
            }

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
            if(!(await ValidationUpdateWalkRequest(walk)))
            {
                return BadRequest(walk);
            }

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

        #region private method
        private async Task<bool>  ValidationAddWalkRequest(Models.DTO.AddWalkRequest walks)
        {
           
            if(walks == null)
            {
                ModelState.AddModelError(nameof(walks), $"{nameof(walks)} cannot be null");
            }

            if (string.IsNullOrEmpty(walks.Name))
            {
                ModelState.AddModelError(nameof(walks),
                 $"{nameof(walks.Name)} Name cant be null");   
                    
            }

           if(walks.Length < 0)
            {
                ModelState.AddModelError(nameof(walks),
                 $"{nameof(walks.Length)} Length cannot be less than zero or equal to zero");

            }
            var region = await repo1.GetAsync(walks.RegionId);
            if(region == null)
            {
                ModelState.AddModelError(nameof(walks),
                  $"{nameof(walks.RegionId)} Regionid is invalid"); ;

            }
            var walkdiff = await WalkDifficulty.GetAsync(walks.WalkDifficultyId);
            if(walkdiff == null)
            {
                ModelState.AddModelError(nameof(walks),
                    $"{nameof(walks.WalkDifficultyId)} Walkdifficultyid id invalid");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }
       
        private async Task<bool> ValidationUpdateWalkRequest(Models.DTO.UpdateWalkRequest updatewalk)
        {
            if(updatewalk == null)
            {
                ModelState.AddModelError(nameof(updatewalk), $"{nameof(updatewalk)} Cannot be null");


            }
            if (string.IsNullOrEmpty(updatewalk.Name))
            {
                ModelState.AddModelError(nameof(updatewalk.Name),
                    $"{nameof(updatewalk.Name)} Name cannot be null");
            }

            if(updatewalk.Length <= 0)
            {
                ModelState.AddModelError(nameof(updatewalk.Length),
                    $"{nameof(updatewalk.Length)} Length should not be less than zero or equal to zero");
            }
           var upwalk = await WalkDifficulty.GetAsync(updatewalk.WalkDifficultyId);
            if(upwalk == null)
            {
                ModelState.AddModelError(nameof(updatewalk.WalkDifficultyId), $"{nameof(updatewalk.WalkDifficultyId)} Invalid difficulty id");
            }

            var updateregion = await repo1.GetAsync(updatewalk.RegionId);
            if(updateregion == null)
            {
                ModelState.AddModelError(nameof(updatewalk.RegionId), $"{nameof(updatewalk.RegionId)} Invalid RegionId");
                   
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion

    }
}
