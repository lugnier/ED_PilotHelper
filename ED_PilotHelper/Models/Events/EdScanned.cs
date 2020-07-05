using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ED_PilotHelper.Models.Events
{
    class EdScanned:EdEventBase
    {
        [JsonProperty("ScanType")]
        public string ScanType { get; set; }
    }
}
