using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdParent
    {
        [JsonProperty("Planet", NullValueHandling = NullValueHandling.Ignore)]
        public long? Planet { get; set; }

        [JsonProperty("Null", NullValueHandling = NullValueHandling.Ignore)]
        public long? Null { get; set; }

        [JsonProperty("Star", NullValueHandling = NullValueHandling.Ignore)]
        public long? Star { get; set; }
    }
}
