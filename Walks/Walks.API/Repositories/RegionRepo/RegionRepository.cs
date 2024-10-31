using Microsoft.EntityFrameworkCore;
using Walks.API.Data.DomainModels;

namespace Walks.API.Repositories.RegionRepo
{
    public class RegionRepository : IRegionRepository
    {

        //Constructor injection.
        private readonly WalksDbContext dbContext;

        public RegionRepository(WalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region?> CreateRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);   
            await dbContext.SaveChangesAsync();
            return region;
            
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync(); 
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            //Find the region.
            var region = await dbContext.Regions.FindAsync(id);
            return region ; 

        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region providedRegion)
        {
            //Find the region associated with the id.
            var region = await GetRegionByIdAsync (id);
            if (region == null) {
                return null; 
            }

            //Update the values.
            region.Name = providedRegion.Name;
            region.Code = providedRegion.Code;
            region.RegionImageUrl = providedRegion.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return region; 
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            //Find the region.
            var region = await dbContext.Regions.FindAsync(id);
            if (region == null)
            {
                return null;
            }

            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;
        }
    }
}
