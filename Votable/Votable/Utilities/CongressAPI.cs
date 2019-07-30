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
        private static readonly string baseURL = @"https://api.propublica.org/congress/v1/116/";
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
                var result = Client.Execute(new RestRequest("senate/members.json"));
                if (result.IsSuccessful)
                {
                    var data = JsonConvert.DeserializeObject<RestResult<Chamber>>(result.Content, serialSettings);
                    Senators = data.Results.First().Members;
                    OnPropertyChanged(nameof(Senators));
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
