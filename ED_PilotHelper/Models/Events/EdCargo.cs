using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdCargo : EdEventBase
    {
        [JsonProperty("Vessel")]
        public string Vessel { get; set; }

        [JsonProperty("Count")]
        public long Count { get; set; }

        [JsonProperty("Inventory")]
        public EdInventory[] Inventory { get; set; }
    }
}
