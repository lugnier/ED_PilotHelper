using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdAsteroidCracked:EdEventBase
    {
        [JsonProperty("Body")]
        public string Body { get; set; }
    }
}
