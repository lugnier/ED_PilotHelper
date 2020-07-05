using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdFSSDiscoveryScan:EdEventBase
    {
        [JsonProperty("Progress")]
        public double Progress { get; set; }

        [JsonProperty("BodyCount")]
        public long BodyCount { get; set; }

        [JsonProperty("NonBodyCount")]
        public long NonBodyCount { get; set; }

        [JsonProperty("SystemName")]
        public string SystemName { get; set; }

        [JsonProperty("SystemAddress")]
        public long SystemAddress { get; set; }
    }
}
