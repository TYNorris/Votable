using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Votable.Models
{
    public class Member
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "short_title")]
        public string TitleAbbreviated { get; set; }

        [JsonProperty(PropertyName = "api_uri")]
        public string API_URL { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middle_name")]
        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "suffix")]
        public string Suffix { get; set; }

        [JsonProperty(PropertyName = "date_of_birth")]
        public string Birthdate { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "party")]
        public string Party { get; set; }

        [JsonProperty(PropertyName = "leadership_role")]
        public string LeadershipRole { get; set; }

        [JsonProperty(PropertyName = "twitter_account")]
        public string Twitter { get; set; }

        [JsonProperty(PropertyName = "facebook_account")]
        public string Facebook { get; set; }
        [JsonProperty(PropertyName = "youtube_account")]
        public string Youtube { get; set; }

        [JsonProperty(PropertyName = "govtrack_id")]
        public string GovTrackID { get; set; }

        [JsonProperty(PropertyName = "cspan_id")]
        public string CSPAN_ID { get; set; }

        [JsonProperty(PropertyName = "votesmart_id")]
        public string voteSmartId { get; set; }

        [JsonProperty(PropertyName = "icpsr_id")]
        public string ICPSR_ID { get; set; }

        [JsonProperty(PropertyName = "crp_id")]
        public string CRP_ID { get; set; }

        [JsonProperty(PropertyName = "google_entity_id")]
        public string Google_Id { get; set; }

        [JsonProperty(PropertyName = "fec_candidate_id")]
        public string FEC_ID { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string WebUrl { get; set; }

        [JsonProperty(PropertyName = "rss_url")]
        public string RssUrl { get; set; }

        [JsonProperty(PropertyName = "contact_form")]
        public string ContactUrl { get; set; }

        [JsonProperty(PropertyName = "in_office")]
        public bool InOffice { get; set; }

        [JsonProperty(PropertyName = "dw_nominate")]
        public double IdeologyScore { get; set; }

        [JsonProperty(PropertyName = "ideal_point")]
        public string IdealPoint { get; set; }

        [JsonProperty(PropertyName = "seniority")]
        public string Seniority { get; set; }

        [JsonProperty(PropertyName = "next_election")]
        public string NextElection { get; set; }

        [JsonProperty(PropertyName = "total_votes")]
        public int TotalVotes { get; set; }

        [JsonProperty(PropertyName = "missed_votes")]
        public int MissedVotes { get; set; }

        [JsonProperty(PropertyName = "total_present")]
        public int TotalPresent { get; set; }

        [JsonProperty(PropertyName = "last_updated")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty(PropertyName = "ocd_id")]
        public string OCD_ID { get; set; }

        [JsonProperty(PropertyName = "office")]
        public string OfficeAddress { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "fax")]
        public string FaxNumber { get; set; }

        [JsonProperty(PropertyName = "state")]

        public string State { get; set; }

        [JsonProperty(PropertyName = "senate_class")]

        public string SenateClass { get; set; }

        [JsonProperty(PropertyName = "state_rank")]

        public string StateRank { get; set; }

        [JsonProperty(PropertyName = "lis_id")]

        public string LIS_ID { get; set; }
        [JsonProperty(PropertyName = "missed_votes_pct")]

        public double MissedVotePercent { get; set; }

        [JsonProperty(PropertyName = "votes_with_party_pct")]
        public string VoteWithPartyPervent { get; set; }

    }
}
