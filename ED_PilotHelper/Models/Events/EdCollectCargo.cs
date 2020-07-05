using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ED_PilotHelper.Models.Events
{
    class EdCollectCargo:EdEventBase
    {
        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Type_Localised")]
        public string TypeLocalised { get; set; }

        [JsonProperty("Stolen")]
        public bool Stolen { get; set; }
    }
}
