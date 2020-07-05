using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdMaterialTraderStats
    {
        [JsonProperty("Trades_Completed")]
        public long TradesCompleted { get; set; }

        [JsonProperty("Materials_Traded")]
        public long MaterialsTraded { get; set; }

        [JsonProperty("Raw_Materials_Traded")]
        public long RawMaterialsTraded { get; set; }

        [JsonProperty("Grade_1_Materials_Traded")]
        public long Grade1_MaterialsTraded { get; set; }

        [JsonProperty("Grade_2_Materials_Traded")]
        public long Grade2_MaterialsTraded { get; set; }

        [JsonProperty("Grade_3_Materials_Traded")]
        public long Grade3_MaterialsTraded { get; set; }

        [JsonProperty("Grade_4_Materials_Traded")]
        public long Grade4_MaterialsTraded { get; set; }

        [JsonProperty("Grade_5_Materials_Traded")]
        public long Grade5_MaterialsTraded { get; set; }
    }
}
