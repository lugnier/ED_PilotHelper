using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdSAAScanComplete:EdEventBase
    {
        [JsonProperty("BodyName")]
        public string BodyName { get; set; }

        [JsonProperty("SystemAddress")]
        public long SystemAddress { get; set; }

        [JsonProperty("BodyID")]
        public long BodyId { get; set; }

        [JsonProperty("ProbesUsed")]
        public long ProbesUsed { get; set; }

        [JsonProperty("EfficiencyTarget")]
        public long EfficiencyTarget { get; set; }
    }
}
