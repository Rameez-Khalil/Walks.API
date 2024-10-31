using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using Walks.API.Data.DomainModels;
using Walks.API.Data.DTOs;
using Walks.API.Repositories.WalkRepo;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;

        public WalksController(IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            try
            {
                var walks = await walkRepository.GetAllWalksAsync();
                if (walks == null || !walks.Any())
                {
                    return NotFound("No walks were located");
                }

                //map the walks back to the DTO.
                var walksDto = new List<WalksDto>();

                foreach (var walk in walks)
                {
                    walksDto.Add(new WalksDto
                    {
                        Description = walk.Description,
                        Length = walk.Length,
                        Name = walk.Name,
                        WalkImageUrl = walk.WalkImageUrl,
                        DifficultyId = walk.DifficultyId,
                        RegionId = walk.RegionId,
                    });
                }

                return Ok(walksDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkDto request)
        {
            //pick the walk data and map it back to the walk domain model.
            if (request == null) return BadRequest("Cannot save an empty walk");

            //map it back to the walk domain model.
            var walkDomain = new Walk
            {
                Name = request.Name,
                Description = request.Description,
                Length = request.Length,
                WalkImageUrl = request.WalkImageUrl,
                RegionId = request.RegionId,
                DifficultyId = request.DifficultyId,
            }; 

            //send it back to th repo.
            var savedWalk = await walkRepository.CreateWalkAsync(walkDomain);

            //check for the nullability.
            if (savedWalk == null)
            {
                return BadRequest("Cannot save the provided data");
            }

            //convert the saved walk back to the DTO rep.
            var walkDto = new WalksDto
            {
                Name = savedWalk.Name,
                Description = savedWalk.Description,
                Length = savedWalk.Length,
                WalkImageUrl = savedWalk.WalkImageUrl,
                RegionId = savedWalk.RegionId,
                DifficultyId = savedWalk.DifficultyId,
            };

            return CreatedAtAction(nameof(GetWalkById), new { id = savedWalk.Id }, walkDto); 

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            //find the walk by id.
            //return not found if there are no walks.
            //return the walk in the form of dto.

            //find the walk.
            var walk = await walkRepository.GetWalkByIdAsync(id);

            //Check for nullability.
            if (walk == null)
            {
                return NotFound("Resource not found"); 
            }

            //convert the walk into the DTO rep.
            var walkDto = new WalksDto
            {
                Name = walk.Name,
                Description = walk.Description,
                Length = walk.Length,
                WalkImageUrl = walk.WalkImageUrl,
                RegionId = walk.RegionId,
                DifficultyId = walk.DifficultyId,
            };

            //return the walk.
            return Ok(walkDto); 
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromBody] AddWalkDto updatedWalk, [FromRoute] Guid id)
        {
            //check for nullability of the data provided.
            if (updatedWalk == null)
            {
                return BadRequest("Provided data cannot be null"); 
            }

            //send this data to the repo.
            //Convert it to the domain model.
            var walkDomain = new Walk
            {
                Name = updatedWalk.Name,
                Description = updatedWalk.Description,
                Length = updatedWalk.Length,
                WalkImageUrl = updatedWalk.WalkImageUrl,
                RegionId = updatedWalk.RegionId,
                DifficultyId = updatedWalk.DifficultyId,
            }; 

            //sending the data to repo.
            var walkSaved = await walkRepository.UpdateWalkAsync(id, walkDomain);


            //check for nullability.
            if (walkSaved == null)
            {
                return BadRequest("Couldn't saved the walk"); 
            }

            //map it back to the DTO.
            var walkDto = new WalksDto
            {

                Name = updatedWalk.Name,
                Description = updatedWalk.Description,
                Length = updatedWalk.Length,
                WalkImageUrl = updatedWalk.WalkImageUrl,
                RegionId = updatedWalk.RegionId,
                DifficultyId = updatedWalk.DifficultyId,
            };

            //Return the result.
            return Ok(walkDto); 
        }


        [HttpDelete]
        [Route("{Guid:id")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            //Find the walk.
            var walk = await walkRepository.GetWalkByIdAsync(id);

            //return the not found status  -404.
            if (walk == null) return NotFound("Resource could not be found");

            //Perform the delete operation.
            var walkDeleted = await walkRepository.DeleteWalkAsync(walk);

            if (walkDeleted == null)
            {
                return BadRequest("Cannot delete the provided walk"); 
            }

            //map the walk back to the DTO.
            var walkDto = new WalksDto
            {

                Name = walkDeleted.Name,
                Description = walkDeleted.Description,
                Length = walkDeleted.Length,
                WalkImageUrl = walkDeleted.WalkImageUrl,
                RegionId = walkDeleted.RegionId,
                DifficultyId = walkDeleted.DifficultyId,
            };


            return Ok(walkDeleted); 


        }
    }
}
