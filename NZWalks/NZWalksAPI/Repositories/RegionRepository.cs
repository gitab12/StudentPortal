using Microsoft.EntityFrameworkCore;
using NZWalksAPI.DBData;
using NZWalksAPI.Models;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository :IRepository    
    {
        private readonly NzWalksDBContext _dbContext;
        public RegionRepository(NzWalksDBContext nzWalksContext)
        {
            this._dbContext = nzWalksContext;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _dbContext.Tb_Region.ToListAsync();
        }
    }
}
