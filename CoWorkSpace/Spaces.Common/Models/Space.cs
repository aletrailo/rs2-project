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

        public List<string> Images { get; set; }
    }
}
