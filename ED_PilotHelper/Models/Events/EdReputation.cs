using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdReputation : EdEventBase
    {
        [JsonProperty("Empire")]
        public long Empire { get; set; }

        [JsonProperty("Federation")]
        public double Federation { get; set; }

        [JsonProperty("Alliance")]
        public double Alliance { get; set; }
    }
}
