using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ED_PilotHelper.Models.Events
{
    class EdLiftoff : EdEventBase
    {
        [JsonProperty("PlayerControlled")]
        public bool PlayerControlled { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }
    }
}
