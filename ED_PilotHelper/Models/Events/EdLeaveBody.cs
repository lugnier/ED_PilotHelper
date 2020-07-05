using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ED_PilotHelper.Models.Events
{
    class EdLeaveBody:EdEventBase
    {
        [JsonProperty("StarSystem")]
        public string StarSystem { get; set; }

        [JsonProperty("SystemAddress")]
        public long SystemAddress { get; set; }

        [JsonProperty("Body")]
        public string Body { get; set; }

        [JsonProperty("BodyID")]
        public long BodyId { get; set; }
    }
}
