using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdSearchAndRescue
    {
        [JsonProperty("SearchRescue_Traded")]
        public long SearchRescueTraded { get; set; }

        [JsonProperty("SearchRescue_Profit")]
        public long SearchRescueProfit { get; set; }

        [JsonProperty("SearchRescue_Count")]
        public long SearchRescueCount { get; set; }
    }
}
