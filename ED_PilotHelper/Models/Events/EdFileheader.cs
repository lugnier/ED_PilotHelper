using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdFileheader:EdEventBase
    {
        [JsonProperty("part")]
        public long Part { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("gameversion")]
        public string Gameversion { get; set; }

        [JsonProperty("build")]
        public string Build { get; set; }
    }
}
