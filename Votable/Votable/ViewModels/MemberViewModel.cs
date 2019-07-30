using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Votable.Models;

namespace Votable.ViewModels
{
    public class MemberViewModel : BaseViewModel
    {
        #region PublicProperties
        public string FullName => _Member.TitleAbbreviated + " " + _Member.FirstName + " " + _Member.LastName;

        public string Subtitle => _Member.Party + " - " + _Member.State;

        public string Position => String.IsNullOrEmpty(LeadershipRole) ? _Member.Title : _Member.LeadershipRole;

        public string LeadershipRole => _Member.LeadershipRole != null? _Member.LeadershipRole : "";

        public double WithPartyPct => _Member.VoteWithPartyPct;

        public double MissedVotePct => _Member.MissedVotePct;

        public string PhoneNumber => _Member.PhoneNumber;
        #endregion

        #region Private Properties
        private Member _Member {get;set;}

        #endregion

        #region Ctor

        public MemberViewModel(Member mem)
        {
            _Member = mem;
        }

        public MemberViewModel()
        {
            _Member = new Member()
            {
                FirstName = "Test",
                LastName = "Member",
                Party = "X",
                State = "DC",
                LeadershipRole = "Designated Survivor",
                NextElection = "2020",
                VoteWithPartyPct = 83.3,
                MissedVotePct = 2.4,
                PhoneNumber = "202-222-2222"
            };
        }
        #endregion
    }
}
