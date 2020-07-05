using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdScan:EdEventBase
    {
        [JsonProperty("ScanType")]
        public string ScanType { get; set; }

        [JsonProperty("BodyName")]
        public string BodyName { get; set; }

        [JsonProperty("BodyID")]
        public long BodyId { get; set; }

        [JsonProperty("Parents")]
        public EdParent[] Parents { get; set; }

        [JsonProperty("StarSystem")]
        public string StarSystem { get; set; }

        [JsonProperty("SystemAddress")]
        public long SystemAddress { get; set; }

        [JsonProperty("DistanceFromArrivalLS")]
        public double DistanceFromArrivalLs { get; set; }

        [JsonProperty("SemiMajorAxis")]
        public double SemiMajorAxis { get; set; }

        [JsonProperty("Eccentricity")]
        public long Eccentricity { get; set; }

        [JsonProperty("OrbitalInclination")]
        public long OrbitalInclination { get; set; }

        [JsonProperty("Periapsis")]
        public long Periapsis { get; set; }

        [JsonProperty("OrbitalPeriod")]
        public double OrbitalPeriod { get; set; }

        [JsonProperty("WasDiscovered")]
        public bool WasDiscovered { get; set; }

        [JsonProperty("WasMapped")]
        public bool WasMapped { get; set; }
    }
}
