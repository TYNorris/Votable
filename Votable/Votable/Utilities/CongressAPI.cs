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
using Votable.Utilities;

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

        public List<Bill> AllBills { get; set; }
         
        public CongressAPI()
        {
            JObject Keys = JObject.Parse( IoC.Get<FileService>().GetEmbeddedResource("Keys.json"));
            Senators = new List<Member>();
            AllBills = new List<Bill>();
            //Add api key and extend timeout
            ProPublica.AddDefaultHeader("X-API-Key", Keys["ProPublica"].ToString());
            googleAPIkey = Keys["Google"].ToString();
            ProPublica.Timeout = 10000;
            GoogleCivics.Timeout = 10000;

            FetchSenators();
            FetchRecentBills();
        }

        private void FetchSenators()
        {
            Task.Run(async () =>
            {
                //query current senators
                var data = await Query<RestResult<Chamber>>(ProPublica, "116/senate/members.json");
                if (data != null)
                {
                    var results = data.Results.First().Members;
                    foreach (var s in results)
                    {
                        Senators.Add(s);
                    }
                    OnPropertyChanged(nameof(Senators));

                }

            });
        }

        private void FetchRecentBills()
        {
            Task.Run(async () =>
            {
                //query current senators
                var data = await Query<RestResult<DatedResult>>(ProPublica, "115/both/bills/updated.json");
                if (data != null)
                {
                    var results = data.Results.First().Bills;
                    foreach (var s in results)
                    {
                        AllBills.Add(s);
                    }
                    OnPropertyChanged(nameof(AllBills));

                }

            });
        }

        public async Task<List<Bill>> BillsByMemberAsync(string memberID)
        {
           return await Task.Run(async () =>
           {
               //querry bills from specific member
               var data = await Query<RestResult<MemberResult>>(ProPublica, "members/" + memberID + "/bills/introduced.json");
               if (data != null)
               {
                   //Get data from result
                   return data.Results.First().Bills;
               }
               else
               {
                   //Return empty result on fail
                   return new List<Bill>();
               }
           });
        }

        public async Task<Bill> BillByIDAsync(string billID)
        {
            return await BillQuery(billID, ".json");
        }

        public async Task<Bill> BillSubjectsByIDAsync(string billID)
        {
            return await BillQuery(billID, "/subjects.json");
        }

        public async Task<List<Vote>> VotesByMemberAsync(string memberID)
        {
            return await Task.Run(async () =>
            {
                //querry bills from specific member
                var voteData = await Query<RestResult<MemberResult>>(ProPublica, "members/" + memberID + "/votes.json");
                if (voteData != null)
                {
                    //Get data from result
                    return voteData.Results.First().Votes;
                }
                else
                {
                    //Return empty result on fail
                    return new List<Vote>();
                }
            });
        }

        public async Task<string> StateCodeByAddress(string address)
        {
            return await Task.Run(async () =>
            {
                //querry bills from specific member
                var response = await Query<JObject>(GoogleCivics, "representatives?levels=country&roles=legislatorUpperBody&address=" + address +
                                                                     "&key=" + googleAPIkey);
                if (response != null)
                {
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


        private async Task<Bill> BillQuery(string billID, string extension)
        {
            var split = billID.Split('-');
            var bill = split[0];
            var number = split[1];
            return await Task<Bill>.Run(async () =>
            {
                //querry bills from specific member
                var data = await Query<RestResult<Bill>>(ProPublica, number + "/bills/" + bill + extension);
                if (data != null)
                {
                    //Get data from result
                    return data.Results.First();
                }
                else
                {
                    //Return empty result on fail
                    return null;
                }
            });
        }

        private static async Task<T> Query<T>(RestClient Client, string extension)
        {
            return await Task.Run(() =>
            {
                //query current senators
                var result = Client.Execute(new RestRequest(extension));
                if (result.IsSuccessful)
                {
                    //Deserialize rest response
                    return JsonConvert.DeserializeObject<T>(result.Content, serialSettings);

                }
                return default;
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
