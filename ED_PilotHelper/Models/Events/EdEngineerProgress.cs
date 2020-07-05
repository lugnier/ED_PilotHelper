using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdEngineerProgress : EdEventBase
    {
        [JsonProperty("Engineers")]
        public EdEngineerProgress_Engineer[] Engineers { get; set; }
    }
}
