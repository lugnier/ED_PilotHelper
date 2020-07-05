using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdRefuelAll : EdEventBase
    {
        [JsonProperty("Cost")]
        public long Cost { get; set; }

        [JsonProperty("Amount")]
        public double Amount { get; set; }
    }
}
