using Microsoft.EntityFrameworkCore;
using Walks.API.Data.DomainModels;
using Walks.API.Data.DTOs;

namespace Walks.API.Repositories.WalkRepo
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WalksDbContext dbContext;

        public WalkRepository(WalksDbContext dbContext)
        {
           
            this.dbContext = dbContext;
        }

        //Get all walks.
         public async Task<List<Walk>> GetAllWalksAsync()
        {
            //Get the list of walks.
            var walks = await dbContext.Walks
                  .Include(w => w.Region)
                  .Include(w => w.Difficulty)
                  .ToListAsync(); 

            return walks;
        }

        //Create walk.
        public async Task<Walk?> CreateWalkAsync(Walk walk)
        {
            //check for the nullability.
            if(walk == null) return null;
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk; 
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            //find the walk.
            var walk = await dbContext.Walks
                .Include(w=>w.Region)
                .Include(w=>w.Difficulty)
                .FirstOrDefaultAsync(x=>x.Id==id);

            //return the walk.
            return walk;
        }

        //update walk.
        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk providedWalk)
        {
            //Find the walk.
            var existingWalk = await dbContext.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null; 
            }

            existingWalk.Name = providedWalk.Name;
            existingWalk.Length = providedWalk.Length;
            existingWalk.WalkImageUrl = providedWalk.WalkImageUrl;
            existingWalk.Description = providedWalk.Description; 
            existingWalk.DifficultyId = providedWalk.DifficultyId;
            existingWalk.RegionId = providedWalk.RegionId;


            await dbContext.SaveChangesAsync();
            return existingWalk; 
        }

        public async Task<Walk?> DeleteWalkAsync(Walk walk)
        {
            //Apply remove method.
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk; 
        }
    }
}
