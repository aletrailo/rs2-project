using System;
using System.Collections.Generic;
using System.Text;

namespace Advertising.Common.Models
{
    public sealed class UsernameSpaceIdInfo
    {
        public string username {  get; set; }

        public string spaceId { get; set; }

        public UsernameSpaceIdInfo(string username, string spaceId)
        {
            this.username = username;
            this.spaceId = spaceId;
        }
    }
}
