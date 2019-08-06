using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Votable.Models;

namespace Votable
{
    public class CongressAPI : INotifyPropertyChanged
    {
        /// <summary>
        /// Base url for propublic API
        /// </summary>
        private static readonly string proPublicaURL = @"https://api.propublica.org/congress/v1/";


        private static readonly string googleCivicsURL = @"https://www.googleapis.com/civicinfo/v2/";


        private static string googleAPIkey = "";

        /// <summary>
        /// API Client to Propublica API
        /// </summary>
        RestClient ProPublica = new RestClient(proPublicaURL);

        RestClient GoogleCivics = new RestClient(googleCivicsURL);

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        private static readonly JsonSerializerSettings serialSettings = new JsonSerializerSettings()
        {
            Error = ErrorHandler,
            NullValueHandling = NullValueHandling.Ignore,
        };

        public List<Member> Senators { get; set; }
         
        public CongressAPI()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(CongressAPI)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Votable.Keys.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            JObject Keys = JObject.Parse( text);
            Senators = new List<Member>();
            //Add api key and extend timeout
            ProPublica.AddDefaultHeader("X-API-Key", Keys["ProPublica"].ToString());
            googleAPIkey = Keys["Google"].ToString();
            ProPublica.Timeout = 10000;
            GoogleCivics.Timeout = 10000;

            FetchSenators();
        }

        private void FetchSenators()
        {
            Task.Run(() =>
            {
                //query current senators
                var result = ProPublica.Execute(new RestRequest("116/senate/members.json"));
                if (result.IsSuccessful)
                {
                    //Deserialize rest response
                    var data = JsonConvert.DeserializeObject<RestResult<Chamber>>(result.Content, serialSettings);
                    var results = data.Results.First().Members;
                    foreach (var s in results)
                    {
                        Senators.Add(s);
                    }
                    OnPropertyChanged(nameof(Senators));

                }

            });
        }

        public async Task<List<Bill>> BillsByMemberAsync(string memberID)
        {
           return await Task.Run(() =>
           {
               //querry bills from specific member
               var billResult = ProPublica.Execute(new RestRequest("members/" + memberID + "/bills/introduced.json"));
               if (billResult.IsSuccessful)
               {
                   //Get data from result
                   var billData = JsonConvert.DeserializeObject<RestResult<MemberResult>>(billResult.Content, serialSettings);
                   return billData.Results.First().Bills;
               }
               else
               {
                   //Return empty result on fail
                   return new List<Bill>();
               }
           });
        }

        public async Task<string> StateCodeByAddress(string address)
        {
            return await Task.Run(() =>
            {
                //querry bills from specific member
                var repResult = GoogleCivics.Execute(new RestRequest("representatives?levels=country&roles=legislatorUpperBody&address=" + address +
                                                                     "&key=" + googleAPIkey));
                if (repResult.IsSuccessful)
                {
                    JObject response = JObject.Parse(repResult.Content);
                    if(response["divisions"] != null)
                    {
                        var div = response["divisions"] as JObject;
                        var location = div.Properties().Select(p => p.Name).First();
                        return location.Split(':').Last();
                    }

                    return "";
                }
                else
                {
                    //Return empty result on fail
                    return "";
                }
            });
        }



        public static void ErrorHandler(object sender, EventArgs e)
        {
           //Handle errors from Client requests 
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
