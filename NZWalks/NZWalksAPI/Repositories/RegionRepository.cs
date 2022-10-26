using Microsoft.EntityFrameworkCore;
using NZWalksAPI.DBData;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository :IRepository    
    {
        private readonly NzWalksDBContext _dbContext;
        public RegionRepository(NzWalksDBContext nzWalksContext)
        {
            this._dbContext = nzWalksContext;
        }

        public async Task<Region> AddRegions(Region reg)
        {
            reg.Id = Guid.NewGuid();
            await _dbContext.AddAsync(reg);
            await _dbContext.SaveChangesAsync();
            return reg;
                
        }

        public async Task<Region> DeleteById(Guid id)
        {
            var regiondelete = await _dbContext.Tb_Region.FirstOrDefaultAsync(x => x.Id == id);
             if(regiondelete == null)
            {
                return null;
            }
             _dbContext.Tb_Region.Remove(regiondelete);
           await _dbContext.SaveChangesAsync();
            return regiondelete;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _dbContext.Tb_Region.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
          return await _dbContext.Tb_Region.FirstOrDefaultAsync(x => x.Id == id);
        }

        

        public async Task<Region> UpdateWalk(Guid id, Region region)
        {
           var walkupdate = await _dbContext.Tb_Region.FirstOrDefaultAsync(x => x.Id == id);
            if(walkupdate == null)
            {
                return null;
            }

            walkupdate.Code= region.Code;
            walkupdate.Area = region.Area;
            walkupdate.Lat = region.Lat;
            walkupdate.Long = region.Long;
            walkupdate.Name = region.Name;
            walkupdate.Population = region.Population;
            await _dbContext.SaveChangesAsync();  
            return walkupdate;
        
        }
    }
}
