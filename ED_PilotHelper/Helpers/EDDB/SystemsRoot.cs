using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_PilotHelper.Helpers.EDDB
{
    public class SystemsRoot
    {
        public List<SystemsDetail> docs { get; set; }

        public int total { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
    }
}
