using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdLoadout : EdEventBase
    {
        [JsonProperty("Ship")]
        public string Ship { get; set; }

        [JsonProperty("ShipID")]
        public long ShipId { get; set; }

        [JsonProperty("ShipName")]
        public string ShipName { get; set; }

        [JsonProperty("ShipIdent")]
        public string ShipIdent { get; set; }

        [JsonProperty("HullValue")]
        public long HullValue { get; set; }

        [JsonProperty("ModulesValue")]
        public long ModulesValue { get; set; }

        [JsonProperty("HullHealth")]
        public long HullHealth { get; set; }

        [JsonProperty("UnladenMass")]
        public double UnladenMass { get; set; }

        [JsonProperty("CargoCapacity")]
        public long CargoCapacity { get; set; }

        [JsonProperty("MaxJumpRange")]
        public double MaxJumpRange { get; set; }

        [JsonProperty("FuelCapacity")]
        public EdFuelCapacity FuelCapacity { get; set; }

        [JsonProperty("Rebuy")]
        public long Rebuy { get; set; }

        [JsonProperty("Modules")]
        public EdModule[] Modules { get; set; }
    }
}
