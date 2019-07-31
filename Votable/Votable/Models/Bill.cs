using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Votable.Models
{
    public class Bill
    {
        #region JSON Properties
        [JsonProperty(PropertyName = "bill_id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "bill_type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "bill_uri")]
        public string URI { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "short_title")]
        public string ShortTitle { get; set; }

        [JsonProperty(PropertyName = "sponsor_id")]
        public string Sponsor_ID { get; set; }

        [JsonProperty(PropertyName = "sponsor_name")]
        public string Sponsor_Name { get; set; }

        [JsonProperty(PropertyName = "gpo_pdf_uri")]
        public string PDF_URI { get; set; }
        [JsonProperty(PropertyName = "congressdotgov_uri")]
        public string Congress_URI { get; set; }

        [JsonProperty(PropertyName = "introduced_date")]
        public DateTime IntroducedDate { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "primary_subject")]
        public string Subject { get; set; }
        #endregion
    }
}
