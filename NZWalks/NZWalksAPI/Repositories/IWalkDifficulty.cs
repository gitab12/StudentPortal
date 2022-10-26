using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalkDifficulty
    {
        public Task<IEnumerable<WalkDifficulty>> GetAll();
        public Task<WalkDifficulty> GetAsync(Guid id);
        // Task<Region> GetAsyncById(Region region);
        public Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty walkreg);
        public Task<WalkDifficulty> DeleteById(Guid id);
        public Task<WalkDifficulty> UpdateWalk(Guid id, WalkDifficulty walkreg);
    }
}
