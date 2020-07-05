using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdProgress : EdEventBase
    {
        [JsonProperty("Combat")]
        public long Combat { get; set; }

        [JsonProperty("Trade")]
        public long Trade { get; set; }

        [JsonProperty("Explore")]
        public long Explore { get; set; }

        [JsonProperty("Empire")]
        public long Empire { get; set; }

        [JsonProperty("Federation")]
        public long Federation { get; set; }

        [JsonProperty("CQC")]
        public long Cqc { get; set; }
    }
}
