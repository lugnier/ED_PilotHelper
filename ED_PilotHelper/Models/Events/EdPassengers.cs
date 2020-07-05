using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdPassengers
    {
        [JsonProperty("Passengers_Missions_Bulk")]
        public long PassengersMissionsBulk { get; set; }

        [JsonProperty("Passengers_Missions_VIP")]
        public long PassengersMissionsVip { get; set; }

        [JsonProperty("Passengers_Missions_Delivered")]
        public long PassengersMissionsDelivered { get; set; }

        [JsonProperty("Passengers_Missions_Ejected")]
        public long PassengersMissionsEjected { get; set; }
    }
}
