using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdFSSSignalDiscovered : EdEventBase
    {
        [JsonProperty("SystemAddress")]
        public long SystemAddress { get; set; }

        [JsonProperty("SignalName")]
        public string SignalName { get; set; }

        [JsonProperty("IsStation")]
        public bool IsStation { get; set; }
    }
}
