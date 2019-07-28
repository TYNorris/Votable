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



        public List<Member> Senators { get; set; }

        public CongressAPI()
        {
            Senators = new List<Member>();
            Client.AddDefaultHeader("X-API-Key", "JrgAgCmlGCEmD0q4CoLRLzQ0IJlMGQntG9X0CqGJ");
            Task.Run(() =>
            {
                var result = Client.Execute<RestResult<Chamber>>(new RestRequest("senate/members.json"));
                if(result.IsSuccessful)
                {
                    Senators = result.Data.Results.First().Members;
                    OnPropertyChanged(nameof(Senators));
                }

            });
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
