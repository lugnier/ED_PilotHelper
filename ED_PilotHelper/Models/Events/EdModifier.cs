using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdModifier
    {
        [JsonProperty("Label")]
        public string Label { get; set; }

        [JsonProperty("Value")]
        public double Value { get; set; }

        [JsonProperty("OriginalValue")]
        public double OriginalValue { get; set; }

        [JsonProperty("LessIsGood")]
        public long LessIsGood { get; set; }
    }
}
