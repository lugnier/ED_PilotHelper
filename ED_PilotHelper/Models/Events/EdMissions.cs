using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdMissions:EdEventBase
    {
        [JsonProperty("Active")]
        public EdActive[] Active { get; set; }

        [JsonProperty("Failed")]
        public object[] Failed { get; set; }

        [JsonProperty("Complete")]
        public object[] Complete { get; set; }
    }
}
