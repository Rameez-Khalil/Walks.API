using System.ComponentModel.DataAnnotations;

namespace Walks.API.Data.DomainModels
{
    public class Walk
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Length { get; set; }

        [Required]
        public string WalkImageUrl { get; set; }


        //Navigation Properties.
        [Required]
        public Guid DifficultyId { get; set; }


        [Required]
        public Guid RegionId { get; set; }



        //Import the Difficulty and Regions.
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
