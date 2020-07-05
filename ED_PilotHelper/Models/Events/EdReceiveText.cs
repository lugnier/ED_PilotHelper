using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdReceiveText
    {
        [JsonProperty("From")]
        public string From { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Channel")]
        public string Channel { get; set; }
    }
}
