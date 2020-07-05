using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdFriends : EdEventBase
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
