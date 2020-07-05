using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdBuyAmmo : EdEventBase
    {
        [JsonProperty("Cost")]
        public long Cost { get; set; }
    }
}
