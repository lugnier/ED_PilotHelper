using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ED_PilotHelper.Models.Events
{
    class EdLaunchSRV:EdEventBase
    {
        [JsonProperty("Loadout")]
        public string Loadout { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("PlayerControlled")]
        public bool PlayerControlled { get; set; }
    }
}
