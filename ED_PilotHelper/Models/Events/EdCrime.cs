using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdCrime
    {
        [JsonProperty("Notoriety")]
        public long Notoriety { get; set; }

        [JsonProperty("Fines")]
        public long Fines { get; set; }

        [JsonProperty("Total_Fines")]
        public long TotalFines { get; set; }

        [JsonProperty("Bounties_Received")]
        public long BountiesReceived { get; set; }

        [JsonProperty("Total_Bounties")]
        public long TotalBounties { get; set; }

        [JsonProperty("Highest_Bounty")]
        public long HighestBounty { get; set; }
    }
}
