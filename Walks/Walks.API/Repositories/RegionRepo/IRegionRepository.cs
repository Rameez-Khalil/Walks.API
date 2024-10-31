using Walks.API.Data.DomainModels;

namespace Walks.API.Repositories.RegionRepo
{
    public interface IRegionRepository
    {
        //Get all the regions.
        public Task<List<Region>> GetAllRegionsAsync();
        //Create region.
        public Task<Region?> CreateRegionAsync(Region region);

        //Find region by id.
        public Task<Region?> GetRegionByIdAsync(Guid id);  

        public Task<Region?> UpdateRegionAsync(Guid id, Region region);

        public Task<Region?> DeleteRegionAsync(Guid id);

    }
}
