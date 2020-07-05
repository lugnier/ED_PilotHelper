using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdProspectedAsteroid:EdEventBase
    {
        [JsonProperty("Materials")]
        public EdMaterial[] Materials { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }

        [JsonProperty("Content_Localised")]
        public string ContentLocalised { get; set; }

        [JsonProperty("Remaining")]
        public long Remaining { get; set; }
    }
}
