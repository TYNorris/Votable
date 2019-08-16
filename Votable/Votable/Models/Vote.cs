using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Votable.Models
{
    public class Vote : BaseModel
    {

        [JsonProperty(PropertyName = "member_id")]
        public string MemberID { get; set; }
        [JsonProperty(PropertyName = "chamber")]
        public string Chamber { get; set; }
        [JsonProperty(PropertyName = "congress")]
        public string CongressNumber { get; set; }
        [JsonProperty(PropertyName = "session")]
        public string session { get; set; }

        [JsonProperty(PropertyName = "roll_call")]
        public int RollCallCount { get; set; }

        [JsonProperty(PropertyName = "vote_uri")]
        public string API_URL { get; set; }

        [JsonProperty(PropertyName = "bill")]
        public Bill Bill { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "time")]
        public DateTime Time { get; set; }


        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        public bool For => Position.Equals("Yes");

        public Vote()
        {

        }
    }
}
