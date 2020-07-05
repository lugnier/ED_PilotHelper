using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdFSDTarget:EdEventBase
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("SystemAddress")]
        public long SystemAddress { get; set; }

        [JsonProperty("StarClass")]
        public string StarClass { get; set; }

        [JsonProperty("RemainingJumpsInRoute")]
        public long RemainingJumpsInRoute { get; set; }
    }
}
