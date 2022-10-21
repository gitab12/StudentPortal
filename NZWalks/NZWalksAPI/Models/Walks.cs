namespace NZWalksAPI.Models
{
    public class Walks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Lenght { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDifficultyId { get; set; }

        public Region Region { get; set; }

        public WalkDifficulty WalkDifficulty { get; set; }


    }
}
