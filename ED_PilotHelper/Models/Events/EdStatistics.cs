using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdStatistics : EdEventBase
    {
        [JsonProperty("Bank_Account")]
        public Dictionary<string, long> BankAccount { get; set; }

        [JsonProperty("Combat")]
        public EdCombat Combat { get; set; }

        [JsonProperty("Crime")]
        public EdCrime Crime { get; set; }

        [JsonProperty("Smuggling")]
        public EdSmuggling Smuggling { get; set; }

        [JsonProperty("Trading")]
        public EdTrading Trading { get; set; }

        [JsonProperty("Mining")]
        public EdMining Mining { get; set; }

        [JsonProperty("Exploration")]
        public Dictionary<string, double> Exploration { get; set; }

        [JsonProperty("Passengers")]
        public EdPassengers Passengers { get; set; }

        [JsonProperty("Search_And_Rescue")]
        public EdSearchAndRescue SearchAndRescue { get; set; }

        [JsonProperty("Crafting")]
        public EdCrafting Crafting { get; set; }

        [JsonProperty("Crew")]
        public EdCrew Crew { get; set; }

        [JsonProperty("Multicrew")]
        public EdMulticrew Multicrew { get; set; }

        [JsonProperty("Material_Trader_Stats")]
        public EdMaterialTraderStats MaterialTraderStats { get; set; }
    }
}
