﻿namespace Walks.API.Data.DTOs
{
    public class AddRegionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}