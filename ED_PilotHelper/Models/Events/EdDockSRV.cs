using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ED_PilotHelper.Models.Events
{
    class EdDockSRV:EdEventBase
    {
        [JsonProperty("ID")]
        public long Id { get; set; }
    }
}
