using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_PilotHelper.Helpers.EDDB
{
    public class SystemsParams
    {
        public enum ParamName
        {
            eddbid,
            name,
            allegiancename,
            governmentname,
            primaryeconomyname,
            power,
            powerstatename,
            permit,
            securityname
        }

        public Dictionary<ParamName,Param> Params { get; } = new Dictionary<ParamName, Param>()
        {

            [ParamName.eddbid] = new Param(nomPublic:"eddbid",nomRequete:"eddbid",description:"EDDB ID.", type: "Integer"),
            [ParamName.name] = new Param(nomPublic:"name", nomRequete: "name", description: "System name", type: "String"),
            [ParamName.allegiancename] = new Param(nomPublic:"allegiancename", nomRequete: "allegiancename", description: "Name of the allegiance.", type: "String"),
            [ParamName.governmentname] = new Param(nomPublic:"governmentname", nomRequete: "governmentname", description: "Name of the government type.", type: "String"),
            [ParamName.primaryeconomyname] = new Param(nomPublic:"primaryeconomyname", nomRequete: "primaryeconomyname", description: "The primary economy of the system.", type: "String"),
            [ParamName.power] = new Param(nomPublic:"power", nomRequete: "power", description: "Comma seperated names of powers in influence in the system.", type: "String"),
            [ParamName.powerstatename] = new Param(nomPublic:"powerstatename", nomRequete: "powerstatename", description: "Comma seperated states of the powers in influence in the system.", type: "St ring"),
            [ParamName.permit] = new Param(nomPublic:"permit", nomRequete: "permit", description: "Whether the system is permit locked.", type: "Boolean"),
            [ParamName.securityname] = new Param(nomPublic:"securityname", nomRequete: "securityname", description: "The name of the security status in the system.", type: "String"),
            //[ParamName.page] = new Param(nomPublic:"page", nomRequete: "page", description: "Page no of response.", type: "Integer")
        };

    }
}
