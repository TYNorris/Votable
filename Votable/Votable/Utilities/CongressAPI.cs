using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Votable.Models;

namespace Votable.Services
{
    class CongressAPI
    {
        private static readonly string baseURL = @"https://api.propublica.org/congress/v1/";
        RestClient Client = new RestClient(baseURL);
        public List<Member> Senators { get; set; }

        public CongressAPI()
        {
            Client.AddDefaultHeader("X-API-Key", "JrgAgCmlGCEmD0q4CoLRLzQ0IJlMGQntG9X0CqGJ");
            Senators = Client.Execute<List<Member>>(new RestRequest("senate/members.json")).Data;
        }
    }
}
