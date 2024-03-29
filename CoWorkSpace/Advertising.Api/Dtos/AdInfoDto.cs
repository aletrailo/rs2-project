﻿namespace Advertising.Api.Dtos
{
    public sealed class AdInfoDto
    {

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsFree { get; set; } = true;

        public int PricePerHour { get; set; }
  
    }
}
