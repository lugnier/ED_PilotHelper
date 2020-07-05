using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdSmuggling
    {
        [JsonProperty("Black_Markets_Traded_With")]
        public long BlackMarketsTradedWith { get; set; }

        [JsonProperty("Black_Markets_Profits")]
        public long BlackMarketsProfits { get; set; }

        [JsonProperty("Resources_Smuggled")]
        public long ResourcesSmuggled { get; set; }

        [JsonProperty("Average_Profit")]
        public double AverageProfit { get; set; }

        [JsonProperty("Highest_Single_Transaction")]
        public long HighestSingleTransaction { get; set; }
    }
}
