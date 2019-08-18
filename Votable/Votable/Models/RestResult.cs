using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Votable.Models
{
    public class RestResult<T>
    {
        #region Public Proerties
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "copyright")]
        public string Copyright { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<T> Results { get; set; }
        #endregion

        #region Constructor
        public RestResult()
        {
        }
        #endregion


    }

    public class MemberResult
    {
        [JsonProperty(PropertyName = "id")]
        public string MemberID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string MemberName { get; set; }

        [JsonProperty(PropertyName = "bills")]
        public List<Bill> Bills { get; set; }

        [JsonProperty(PropertyName = "votes")]
        public List<Vote> Votes { get; set; }
    }

    public class DatedResult
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "bills")]
        public List<Bill> Bills { get; set; }
    }
}
