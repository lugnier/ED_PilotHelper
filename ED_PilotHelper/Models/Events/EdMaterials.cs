using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdMaterials : EdEventBase
    {
        [JsonProperty("Raw")]
        public EdMaterials_Raw[] Raw { get; set; }

        [JsonProperty("Manufactured")]
        public EdMaterials_Encoded[] Manufactured { get; set; }

        [JsonProperty("Encoded")]
        public EdMaterials_Encoded[] Encoded { get; set; }
    }
}
