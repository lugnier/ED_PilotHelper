using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ED_PilotHelper.Models.Events
{
    class EdNavRouteDetails:EdEventBase
    {
        [JsonProperty("Route")]
        public EdRoute[] Routes { get; set; }
    }
}
