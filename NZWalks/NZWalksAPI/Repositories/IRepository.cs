using NZWalksAPI.DBData;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IRepository 
    {
        public Task<IEnumerable<Region>> GetAll();
        public Task<Region> GetAsync(Guid id);
        // Task<Region> GetAsyncById(Region region);
        public Task<Region> AddRegions(Region rego);
        public Task<Region> DeleteById(Guid id);
      public  Task<Region> UpdateWalk(Guid id, Region region);


    }
}
