using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdSAASignalsFound:EdEventBase
    {
        [JsonProperty("BodyName")]
        public string BodyName { get; set; }

        [JsonProperty("SystemAddress")]
        public long SystemAddress { get; set; }

        [JsonProperty("BodyID")]
        public long BodyId { get; set; }

        [JsonProperty("Signals")]
        public EdSignal[] Signals { get; set; }
    }
}
