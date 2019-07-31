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
        private static readonly string baseURL = @"https://api.propublica.org/congress/v1/";
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
            Client.AddDefaultHeader("X-API-Key", "JrgAgCmlGCEmD0q4CoLRLzQ0IJlMGQntG9X0CqGJ");
            Client.Timeout = 60000;
            Task.Run(() =>
            {
                var result = Client.Execute(new RestRequest("116/senate/members.json"));
                if (result.IsSuccessful)
                {
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
               var billResult = Client.Execute(new RestRequest("members/" + memberID + "/bills/introduced.json"));
               if (billResult.IsSuccessful)
               {
                   var billData = JsonConvert.DeserializeObject<RestResult<MemberResult>>(billResult.Content, serialSettings);
                   return billData.Results.First().Bills;
               }
               else
               {
                   return new List<Bill>();
               }
           });
        }

        public static void ErrorHandler(object sender, EventArgs e)
        {
            
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
