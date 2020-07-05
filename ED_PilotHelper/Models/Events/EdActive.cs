using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdActive
    {
        [JsonProperty("MissionID")]
        public long MissionId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("PassengerMission")]
        public bool PassengerMission { get; set; }

        [JsonProperty("Expires")]
        public long Expires { get; set; }
    }
}
