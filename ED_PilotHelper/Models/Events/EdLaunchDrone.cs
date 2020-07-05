using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdLaunchDrone:EdEventBase
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}
