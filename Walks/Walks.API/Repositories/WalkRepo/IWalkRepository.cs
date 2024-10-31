using Walks.API.Data.DomainModels;

namespace Walks.API.Repositories.WalkRepo
{
    public interface IWalkRepository
    {
        //Get all walks.
        public Task<List<Walk>> GetAllWalksAsync();

        //Create walk.
        public Task<Walk?> CreateWalkAsync(Walk walk);

        //Get walk by id.
        public Task<Walk?> GetWalkByIdAsync(Guid id); 

        //Update walk.
        public Task<Walk?> UpdateWalkAsync(Guid id , Walk walk);

        //Delete walk.
        public Task<Walk?> DeleteWalkAsync(Walk wlk); 
    }
}
