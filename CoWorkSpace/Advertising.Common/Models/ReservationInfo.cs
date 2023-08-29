using System;
using System.Collections.Generic;
using System.Text;

namespace Advertising.Common.Models
{
    public sealed class ReservationInfo
    {
        public string username {  get; set; }

        public string spaceId { get; set; }

        public ReservationInfo(string username, string spaceId)
        {
            this.username = username;
            this.spaceId = spaceId;
        }
    }
}
