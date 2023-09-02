using System;

namespace Spaces.Common.Models
{
    public sealed class Space
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public Guid ImageId { get; set; }

        public bool IsFree { get; set; }

        public int PricePerHour { get; set; }

        public string Owner { get; set; } 

        public string ReservedBy { get; set; }
    }
}
