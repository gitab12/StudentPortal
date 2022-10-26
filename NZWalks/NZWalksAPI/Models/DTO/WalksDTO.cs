using System.ComponentModel.DataAnnotations.Schema;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Models.DTO
{
    public class WalksDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        // Navigation Properties
        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }

    }
}
