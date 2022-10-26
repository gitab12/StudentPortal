using Microsoft.EntityFrameworkCore;
using NZWalksAPI.DBData;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        
        private readonly NzWalksDBContext Walkdb;
        public WalksRepository(NzWalksDBContext Walkdb)
        {
            this.Walkdb = Walkdb;   
        }

        public async Task<IEnumerable<Walks>> GetWalksAll()
        {
            return await Walkdb.
                Tb_Walks.
                Include(x => x.Region).
                Include(x => x.WalkDifficulty).
                ToListAsync();
        }



        public async Task<Walks> GetWalksAsync(Guid id)
        {
            return await Walkdb.Tb_Walks.
                Include(x => x.Region).
                Include(x => x.WalkDifficulty).
                FirstOrDefaultAsync(x => x.Id == id);
        }

        
        public async Task<Walks> AddWalks(Walks wal)
        {
            wal.Id = Guid.NewGuid();
            await Walkdb.Tb_Walks.AddAsync(wal);
            await Walkdb.SaveChangesAsync();
            return wal;

        }

        public async Task<Walks> DeleteWalksById(Guid id)
        {
            var deletewalk = await Walkdb.Tb_Walks.FirstOrDefaultAsync(x=>x.Id == id);
            if(deletewalk == null)
            {
                return null;
            }
             Walkdb.Tb_Walks.Remove(deletewalk);
            await Walkdb.SaveChangesAsync();
            return deletewalk;
        }

       

        public async Task<Walks> UpdateWalk(Guid id, Walks rewalks)
        {
            var updatewalk = await Walkdb.Tb_Walks.FirstOrDefaultAsync(x => x.Id == id);

            if(updatewalk == null)
            {
                return null;
            }
            updatewalk.Name = rewalks.Name;
            updatewalk.Length = rewalks.Length;
            updatewalk.Region = rewalks.Region;
            updatewalk.WalkDifficulty = rewalks.WalkDifficulty;
            await Walkdb.SaveChangesAsync();
            return updatewalk;

        }
    }
}
