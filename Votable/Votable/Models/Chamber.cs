using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Votable.Models
{
    public class Chamber : BaseModel
    {
        #region Public Properties
        [JsonProperty(PropertyName = "congress")]
        public int Number { get; set; }

        [JsonProperty(PropertyName = "chamber")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "members")]
        public List<Member> Members { get; set; }
        #endregion Public Properties 
        public Chamber()
        {
            Members = new List<Member>();
        }
    }
}
