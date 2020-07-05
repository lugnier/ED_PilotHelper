using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ED_PilotHelper.Helpers.EDDB
{
    public class EddbHelper
    {
        private static String _header = "https://eddbapi.kodeblox.com/api/v4/";

        /// <summary>
        /// récupération du système
        /// </summary>
        /// <param name="systemsParams"></param>
        /// <returns></returns>
        public static async Task<List<SystemsDetail>> GetSystems(SystemsParams systemsParams)
        {
            HttpResponseMessage response;
            string responseBody;
            string query = string.Empty;
            Int32 pageEnCours = 1;
            Int32 pageMax = 1;

            List<SystemsDetail> systems = new List<SystemsDetail>();

            try
            {
                if (App.Client.BaseAddress == null)
                {
                    App.Client.BaseAddress = new Uri(_header);
                }

                foreach (KeyValuePair<SystemsParams.ParamName, Param> systemsParamsParam in systemsParams.Params)
                {
                    if (systemsParamsParam.Value.Valeur != null)
                    {
                        if (String.IsNullOrEmpty(query))
                        {
                            query = "?";
                        }
                        else
                        {
                            query += "&";
                        }
                        query += $"{systemsParamsParam.Value.NomRequete}={systemsParamsParam.Value.Valeur.ToString()}";
                    }
                }

                if (String.IsNullOrEmpty(query))
                {
                    return systems;
                }

                response = await App.Client.GetAsync($"systems{query}");
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();

                SystemsRoot systemsRoot = JsonConvert.DeserializeObject<SystemsRoot>(responseBody);
                pageMax = systemsRoot.pages;
                pageEnCours = systemsRoot.page;

                do
                {
                    systems.AddRange(systemsRoot.docs);

                    if (pageEnCours < pageMax)
                    {
                        pageEnCours++;
                        String s = $"systems{query}&page={pageEnCours}";
                        response = await App.Client.GetAsync(s);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();

                        systemsRoot = JsonConvert.DeserializeObject<SystemsRoot>(responseBody);
                    }

                } while (pageEnCours < pageMax);
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }

            return systems;
        }


        public static async Task<List<StationsDetail>> GetStations(StationsParams stationsParams)
        {
            HttpResponseMessage response;
            string responseBody;
            string query = string.Empty;
            Int32 pageEnCours = 1;
            Int32 pageMax = 1;

            List<StationsDetail> systems = new List<StationsDetail>();

            try
            {
                if (App.Client.BaseAddress == null)
                {
                    App.Client.BaseAddress = new Uri(_header);

                }

                foreach (KeyValuePair<StationsParams.ParamName, Param> stationsParamsParam in stationsParams.Params)
                {
                    if (stationsParamsParam.Value.Valeur != null)
                    {
                        if (String.IsNullOrEmpty(query))
                        {
                            query = "?";
                        }
                        else
                        {
                            query += "&";
                        }
                        query += $"{stationsParamsParam.Value.NomRequete}={stationsParamsParam.Value.Valeur.ToString()}";
                    }
                }

                if (String.IsNullOrEmpty(query))
                {
                    return systems;
                }

                response = await App.Client.GetAsync($"stations{query}");
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                App.Client.CancelPendingRequests();

                StationsRoot stationsRoot = JsonConvert.DeserializeObject<StationsRoot>(responseBody);
                pageMax = stationsRoot.pages;
                pageEnCours = stationsRoot.page;

                do
                {
                    systems.AddRange(stationsRoot.docs);

                    if (pageEnCours < pageMax)
                    {
                        pageEnCours++;
                        String s = $"stations{query}&page={pageEnCours}";
                        response = await App.Client.GetAsync(s);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();

                        stationsRoot = JsonConvert.DeserializeObject<StationsRoot>(responseBody);
                    }

                } while (pageEnCours < pageMax);
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }

            return systems;
        }



        //public async void Test()
        //{
        //    HttpResponseMessage response;
        //    string responseBody;

        //    //response = await App.client.GetAsync("https://eddbapi.kodeblox.com/api/v4/systems?name=DROMI");
        //    //response.EnsureSuccessStatusCode();
        //    //responseBody = await response.Content.ReadAsStringAsync();

        //    //var r = JsonConvert.DeserializeObject<SystemsRoot>(responseBody);

        //showId / showCoordinates / showPermit / showInformation / showPrimaryStar
        //    response = await App.client.GetAsync("https://www.edsm.net/api-v1/sphere-systems?systemName=Andhrimi&radius=100&showPermit=1&showId=1&showCoordinates=1&showInformation=1&showPrimaryStar=1");
        //    response = await App.client.GetAsync("https://www.edsm.net/api-v1/system?systemName=Andhrimi&showPermit=1&showId=1&showCoordinates=1&showInformation=1&showPrimaryStar=1");
        //    response.EnsureSuccessStatusCode();
        //    responseBody = await response.Content.ReadAsStringAsync();

        //    List<SystemSphereRoot> r2 = JsonConvert.DeserializeObject<List<SystemSphereRoot>>(responseBody);


        //    String s1 =
        //        "kalak,mangkhwal,Piscium Sector BQ-Y b0,Volkhabe,Guite,Valtys,Agastani,Anayol,Arexe,Aymariyot,BD+15 3090,Caraceni,Cherets,Coeus,Corbin,CU Cancri,Dagdhangjel,Duamta,Eotienses,Faroras,GM Cephei,HIP 17694,HIP 2453,HIP 53879,Lembava,LHS 1240,LHS 2430,LP 726-6,Luhman,Lung,Miola,NGC 7822,Ribhniugo,Ross,Sadr Region Sector GW-W c1-22,Wailla Sakh	105,Wolf 1329,Zaragas,Zhou Maola";

        //    List<String> systems = s1.Split(',').ToList();

        //    //List<String> systems = new List<string>()
        //    //{
        //    //    "kalak","mangkhwal","Piscium Sector BQ-Y b0","Volkhabe","Guite","Wailladyan","Duamta"
        //    //};


        //    var d2 = r2.Join(systems, x => x.name, y => y, (x, y) => x);

        //    //response = await App.client.GetAsync("https://www.edsm.net/api-v1/systems?systemName[]=Andhrimi&systemName[]=KALAK&showCoordinates=1&showInformation=1&showId=1&showPermit=1&showPrimaryStar=1");
        //    //response.EnsureSuccessStatusCode();
        //    //responseBody = await response.Content.ReadAsStringAsync();

        //    //var c3 = JsonConvert.DeserializeObject<List<Models.edsm.SystemInfoRoot>>(responseBody);



        //    ////var dist = Math.Abs(Math.Sqrt((c3[1].coords.x - c3[0].coords.x) ^ 2 + (c3[1].coords.y - c3[0].coords.y) ^
        //    ////                              2 + (c3[1].coords.z - c3[0].coords.z) ^ 2));

        //    //var dist = Math.Sqrt(Math.Pow(Math.Abs(c3[1].coords.x - c3[0].coords.x), 2) + Math.Pow(Math.Abs(c3[1].coords.y - c3[0].coords.y), 2) + Math.Pow(Math.Abs(c3[1].coords.z - c3[0].coords.z), 2));

        //    Debugger.Break();
        //}
    }
}
