using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdLoadGame : EdEventBase
    {
        [JsonProperty("FID")]
        public string Fid { get; set; }

        [JsonProperty("Commander")]
        public string Commander { get; set; }

        [JsonProperty("Horizons")]
        public bool Horizons { get; set; }

        [JsonProperty("Ship")]
        public string Ship { get; set; }

        [JsonProperty("ShipID")]
        public long ShipId { get; set; }

        [JsonProperty("ShipName")]
        public string ShipName { get; set; }

        [JsonProperty("ShipIdent")]
        public string ShipIdent { get; set; }

        [JsonProperty("FuelLevel")]
        public double FuelLevel { get; set; }

        [JsonProperty("FuelCapacity")]
        public long FuelCapacity { get; set; }

        [JsonProperty("GameMode")]
        public string GameMode { get; set; }

        [JsonProperty("Credits")]
        public long Credits { get; set; }

        [JsonProperty("Loan")]
        public long Loan { get; set; }
    }
}
