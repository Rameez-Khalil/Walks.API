using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Walks.API.Data.DomainModels;
using Walks.API.Data.DTOs;
using Walks.API.Repositories.RegionRepo;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
       

        //Inject the repo.
        private readonly IRegionRepository _regionRepository;
        public RegionsController(IRegionRepository regionRepository)
        {
            this._regionRepository = regionRepository;
        }

        //Getting the list of regions.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Call to the DB.
            try
            {
                var regions = await _regionRepository.GetAllRegionsAsync();
                if (regions == null || !regions.Any())
                {
                    return NotFound();
                }
                else
                {
                    //map it back to the DTO.
                    var regionDto = new List<RegionDto>();
                    //Read the regions list.
                    foreach (var region in regions)
                    {
                        regionDto.Add(new RegionDto()
                        {
                            Name = region.Name,
                            Code = region.Code,
                            RegionImageUrl = region.RegionImageUrl,

                        });
                    }

                    return Ok(regionDto);
                }
            }
            catch
            {
                return StatusCode(500, "resource not found - catch block");
            }

        }


        //Creating a resource.
        [HttpPost]
        public async Task<IActionResult> CreateRegion(RegionDto region)
        {
            //Accept the data and check for its validity.
            //Send the data back to the repo.
            //Based off of the results retuned by the repo, eithe transfer to the created at, or return 500.


            try
            {
                //accept the data.
                if (region == null)
                {
                    return BadRequest("Region cannot be null");

                }

                //Convert the DTO to its domain.
                var regionDomain = new Region
                {
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                };



                //sending it to the repo.
                var regionSaved = await _regionRepository.CreateRegionAsync(regionDomain);

                //Check for the respone returned by the repo.
                if (regionSaved == null)
                {
                    return StatusCode(500, "Region could not be saved due to an error at the server level");

                }

                //Convert the regionDomain to the DTO.
                var savedRegionDto = new AddRegionDto
                {
                    Id = regionSaved.Id,
                    Name = regionSaved.Name,
                    Code = regionSaved.Code,
                    RegionImageUrl = regionSaved.RegionImageUrl,
                };

                return CreatedAtAction(nameof(GetRegionById), new { id = savedRegionDto.Id }, savedRegionDto);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Error occured at the time of saving");
            }


        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            try
            {

                //get the id and send it to the repo.
                var region = await _regionRepository.GetRegionByIdAsync(id);
                if (region == null)
                {
                    return NotFound("No region is associated with the provided ID");
                }

                //Map the region back to the DTO.
                var regionDto = new RegionDto
                {
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                };

                return Ok(regionDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occured while finding the region");
            }

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromBody] UpdateRegionDto request, [FromRoute] Guid id)
        {
            //Find the region through Id.
            //send the region to the repo to be update with the newly provided values.

            try
            {
                //check for the data validity.
                if (request==null)
                {
                    return BadRequest("Provided data cannot be null"); 
                }

                //Accept the data and convert it to the region domain.
                var regionDomain = new Region
                {
                    Name = request.Name,
                    Code = request.Code,
                    RegionImageUrl = request.RegionImageUrl,
                };

                //Send this data to the repo.
                var regionUpdated = await _regionRepository.UpdateRegionAsync(id, regionDomain);

                //Check for null.
                if (regionUpdated == null)
                {
                    //return bad request.
                    return BadRequest("Provided data cannot be overridden");
                }

                //Map it back to the regionUpdated.
                var regionDto = new RegionDto
                {
                    Name = regionUpdated.Name,
                    Code = regionUpdated.Code,
                    RegionImageUrl = regionUpdated.RegionImageUrl,
                };


                return Ok(regionDto);
            }

            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occured"); 
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            //Send the id to the repo.
            try
            {
                var regionDeleted = await _regionRepository.DeleteRegionAsync(id);
                if (regionDeleted == null)
                {
                    return NotFound("Resource cannot be located");
                }

                return Ok();
            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message);
                return StatusCode(500, "Resource not found"); 
            }
        }
    }
}
