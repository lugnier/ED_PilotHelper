using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_PilotHelper.Helpers
{
    public class Param
    {
        public String NomPublic { get; }
        public String NomRequete { get;}
        public String Description { get;}
        public String Type { get; }
        public Object Valeur { get; set; } = null;

        public Param(String nomPublic, String nomRequete, String description, String type)
        {
            NomPublic = nomPublic;
            NomRequete = nomRequete;
            Description = description;
            Type = type;
        }

    }
}
