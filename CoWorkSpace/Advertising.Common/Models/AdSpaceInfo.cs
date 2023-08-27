using System;
using System.Collections.Generic;
using System.Text;

namespace Advertising.Common.Models
{
    public sealed class AdSpaceInfo
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsFree { get; set; }
    }
}
