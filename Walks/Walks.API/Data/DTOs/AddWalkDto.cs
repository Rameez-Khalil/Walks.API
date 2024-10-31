namespace Walks.API.Data.DTOs
{
    public class AddWalkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Length { get; set; }
        public string WalkImageUrl { get; set; }


        //Navigation Properties.
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
