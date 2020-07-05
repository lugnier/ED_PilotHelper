using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdFuelCapacity
    {
        [JsonProperty("Main")]
        public long Main { get; set; }

        [JsonProperty("Reserve")]
        public double Reserve { get; set; }
    }
}
