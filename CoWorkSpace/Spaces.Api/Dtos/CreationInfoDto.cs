﻿namespace Spaces.Api.Dtos
{
    public class CreationInfoDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsFree { get; set; }

        public int PricePerHour { get; set; }

        public string Owner { get; set; } = string.Empty;

    }
}
