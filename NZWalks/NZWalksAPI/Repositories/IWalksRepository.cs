using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalksRepository
    {
        public Task<IEnumerable<Walks>> GetWalksAll();
        public Task<Walks> GetWalksAsync(Guid id);
        // Task<Region> GetAsyncById(Region region);
        public Task<Walks> AddWalks(Walks rego);
        public Task<Walks> DeleteWalksById(Guid id);
        public Task<Walks> UpdateWalk(Guid id, Walks rewalks);

    }
}
