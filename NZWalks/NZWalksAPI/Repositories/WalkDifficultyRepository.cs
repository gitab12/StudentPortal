using Microsoft.EntityFrameworkCore;
using NZWalksAPI.DBData;
using NZWalksAPI.Migrations;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficulty
    {

        private readonly NzWalksDBContext _dbContext;
        public WalkDifficultyRepository(NzWalksDBContext dBContext)
        {
          _dbContext = dBContext;
        }

        public  async Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty walkreg)
        {
           walkreg.Id = Guid.NewGuid();
            await _dbContext.Tb_WalkDifficulties.AddAsync(walkreg);
            await _dbContext.SaveChangesAsync();
            return walkreg;

        }

        

        public async Task<IEnumerable<WalkDifficulty>> GetAll()
        {
           
            return await _dbContext.Tb_WalkDifficulties.ToListAsync();

        }

        public Task<WalkDifficulty> GetAsync(Guid id)
        {
            return _dbContext.Tb_WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
             


        }

        public async Task<WalkDifficulty> UpdateWalk(Guid id, WalkDifficulty walkreg)
        {
            var walkre = _dbContext.Tb_WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if(walkre == null)
            {
                return null;
            }
             _dbContext.Tb_WalkDifficulties.Remove(walkreg);
            _dbContext.SaveChanges();
            return walkreg;
        }


        public async Task<WalkDifficulty> DeleteById(Guid id)
        {
            var walkdel = await _dbContext.Tb_WalkDifficulties.FindAsync(id);
            if(walkdel != null)
            {
                _dbContext.Tb_WalkDifficulties.Remove(walkdel);
                await _dbContext.SaveChangesAsync();
                return walkdel;
            }
            return null;
            


        }
    }
}
