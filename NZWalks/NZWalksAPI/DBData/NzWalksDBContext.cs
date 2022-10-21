using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models;

namespace NZWalksAPI.DBData
{
    public class NzWalksDBContext :DbContext
    {

        public NzWalksDBContext(DbContextOptions<NzWalksDBContext> options) : base(options)
        {

        }
        public DbSet<Region> Tb_Region { get; set; }
        public DbSet<Walks> Tb_Walks { get; set; }

        public DbSet<WalkDifficulty> Tb_WalkDifficulties { get; set; }
    }
}
