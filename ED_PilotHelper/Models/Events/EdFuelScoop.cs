using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdFuelScoop:EdEventBase
    {
        [JsonProperty("Scooped")]
        public double Scooped { get; set; }

        [JsonProperty("Total")]
        public long Total { get; set; }
    }
}
