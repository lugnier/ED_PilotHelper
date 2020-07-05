using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdMusic : EdEventBase
    {
        [JsonProperty("MusicTrack")]
        public string MusicTrack { get; set; }
    }
}
