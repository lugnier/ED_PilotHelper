using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdCommander : EdEventBase
    {
        [JsonProperty("FID")]
        public string Fid { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
