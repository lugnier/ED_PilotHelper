using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_PilotHelper.Helpers.EDDB
{
    public class SystemsDetail
    {
        public string _id { get; set; }
        public int id { get; set; }
        public string name_lower { get; set; }
        public string reserve_type { get; set; }
        public int? reserve_type_id { get; set; }
        public string controlling_minor_faction { get; set; }
        public int? controlling_minor_faction_id { get; set; }
        public string simbad_ref { get; set; }
        public DateTime updated_at { get; set; }
        public bool needs_permit { get; set; }
        public int? power_state_id { get; set; }
        public string power_state { get; set; }
        public string power { get; set; }
        public string primary_economy { get; set; }
        public int primary_economy_id { get; set; }
        public string security { get; set; }
        public int security_id { get; set; }
        public string allegiance { get; set; }
        public int allegiance_id { get; set; }
        public string government { get; set; }
        public int government_id { get; set; }
        public bool is_populated { get; set; }
        public long? population { get; set; }
        public double z { get; set; }
        public double y { get; set; }
        public double x { get; set; }
        public string name { get; set; }
        public int? edsm_id { get; set; }
        public int __v { get; set; }
    }
}
