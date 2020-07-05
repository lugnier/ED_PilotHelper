using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_PilotHelper.Helpers.EDDB
{
    

    public class StationsParams 
    {
        public enum ParamName
        {
            eddbid,
            name,
            ships,
            moduleid,
            controllingfactionname,
            statenames,
            allegiancename,
            governmentname,
            minlandingpad,
            distancestar,
            facilities,
            commodities,
            stationtypename,
            planetary,
            economyname,
            permit,
            power,
            powerstatename,
            systemname
        }


        public Dictionary<ParamName, Param> Params { get; } = new Dictionary<ParamName, Param>()
        {

            [ParamName.eddbid] = new Param(nomPublic: "eddbid", nomRequete: "eddbid", description: "EDDB ID", type: "Int64"),
            [ParamName.name] = new Param(nomPublic: "name", nomRequete: "name", description: "Station name.", type: "String"),
            [ParamName.ships] = new Param(nomPublic: "ships", nomRequete: "ships", description: "Comma seperated names of ships sold.", type: "String"),
            [ParamName.moduleid] = new Param(nomPublic: "moduleid", nomRequete: "moduleid", description: "Comma seperated ids of modules sold.", type: "String"),
            [ParamName.controllingfactionname] = new Param(nomPublic: "controllingfactionname", nomRequete: "controllingfactionname", description: "Name of the controlling minor faction.", type: "String"),
            [ParamName.statenames] = new Param(nomPublic: "statenames", nomRequete: "statenames", description: "Comma seperated names of states.", type: "String"),
            [ParamName.allegiancename] = new Param(nomPublic: "allegiancename", nomRequete: "allegiancename", description: "Name of the allegiance.", type: "String"),
            [ParamName.governmentname] = new Param(nomPublic: "governmentname", nomRequete: "governmentname", description: "Name of the government type.", type: "String"),
            [ParamName.minlandingpad] = new Param(nomPublic: "minlandingpad", nomRequete: "minlandingpad", description: "Minimum landing pad size available.", type: "String"),
            [ParamName.distancestar] = new Param(nomPublic: "distancestar", nomRequete: "distancestar", description: "Maximum distance from the star.", type: "String"),
            [ParamName.facilities] = new Param(nomPublic: "facilities", nomRequete: "facilities", description: "Comma seperated names of facilities available in the station.", type: "Boolean"),
            [ParamName.commodities] = new Param(nomPublic: "commodities", nomRequete: "commodities", description: "Comma seperated names of commodities available.", type: "String"),
            [ParamName.stationtypename] = new Param(nomPublic: "stationtypename", nomRequete: "stationtypename", description: "Comma seperated types of station.", type: "String"),
            [ParamName.planetary] = new Param(nomPublic: "planetary", nomRequete: "planetary", description: "Whether the station is planetary.", type: "Boolean"),
            [ParamName.economyname] = new Param(nomPublic: "economyname", nomRequete: "economyname", description: "The economy of the station.", type: "String"),
            [ParamName.permit] = new Param(nomPublic: "permit", nomRequete: "permit", description: "Whether the system where the station exists is permit locked.", type: "Boolean"),
            [ParamName.power] = new Param(nomPublic: "power", nomRequete: "power", description: "Comma seperated names of powers in influence in the system the station is in.", type: "String"),
            [ParamName.powerstatename] = new Param(nomPublic: "powerstatename", nomRequete: "powerstatename", description: "Comma seperated states of the powers in influence in the system the station is in.", type: "String"),
            [ParamName.systemname] = new Param(nomPublic: "systemname", nomRequete: "systemname", description: "Name of the system the station is in.", type: "String"),
        };
    }
}
