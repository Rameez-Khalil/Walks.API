using Org.BouncyCastle.Asn1.Cmp;
using System.ComponentModel.DataAnnotations;

namespace Walks.API.Data.DTOs
{
    public class RegionDto
    {

        [Required]
        [MinLength(5, ErrorMessage= "Min length should of 3 characters")]
        [MaxLength(100, ErrorMessage ="Max length cannot exceed 3 characters")]

        public string Name { get; set; }


        [Required]
        [MinLength(3, ErrorMessage ="Min length should be of at least 3 characters")]
        [MaxLength(3, ErrorMessage ="Max length should be of at least 3 characters")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
