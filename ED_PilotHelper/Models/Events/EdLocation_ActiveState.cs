using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdLocation_ActiveState
    {
        [JsonProperty("State")]
        public string State { get; set; }
    }
}
