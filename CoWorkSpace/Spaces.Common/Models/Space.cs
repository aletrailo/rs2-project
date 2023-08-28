using System.Collections.Generic;
using System.Linq;

namespace Spaces.Common.Models
{
    public sealed class Space
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Image { get; set; } //url slike prostora

        public bool IsFree { get; set; }

        public int PricePerHour { get; set; }

        public string Owner { get; set; } 

        public string ReservedBy { get; set; }
    }
}
