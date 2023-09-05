using System;
using System.Collections.Generic;
using System.Text;

namespace Spaces.Common.Models
{
    public sealed class CreationInfo
    {

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Image { get; set; } 

        public bool IsFree { get; set; }

        public int PricePerHour { get; set; }

        public string Owner { get; set; }

    }
}
