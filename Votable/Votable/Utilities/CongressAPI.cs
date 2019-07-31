using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private static readonly string baseURL = @"https://api.propublica.org/congress/v1/";
        /// <summary>
        /// API Client to Propublica API
        /// </summary>
        RestClient Client = new RestClient(baseURL);

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        private static readonly JsonSerializerSettings serialSettings = new JsonSerializerSettings()
        {
            Error = ErrorHandler,
            NullValueHandling = NullValueHandling.Ignore,
        };

        public List<Member> Senators { get; set; }
         
        public CongressAPI()
        {
            Senators = new List<Member>();
            //Add api key and extend timeout
            Client.AddDefaultHeader("X-API-Key", "JrgAgCmlGCEmD0q4CoLRLzQ0IJlMGQntG9X0CqGJ");
            Client.Timeout = 60000;
            Task.Run(() =>
            {
                //query current senators
                var result = Client.Execute(new RestRequest("116/senate/members.json"));
                if (result.IsSuccessful)
                {
                    //Deserialize rest response
                    var data = JsonConvert.DeserializeObject<RestResult<Chamber>>(result.Content, serialSettings);
                    var results = data.Results.First().Members;
                    foreach(var s in results)
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
               var billResult = Client.Execute(new RestRequest("members/" + memberID + "/bills/introduced.json"));
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
