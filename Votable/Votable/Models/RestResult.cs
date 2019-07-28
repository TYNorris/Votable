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
}
