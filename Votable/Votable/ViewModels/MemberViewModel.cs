using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Votable.Base;
using Votable.Models;
using Votable.Utilities;
using Votable.Views;
using Xamarin.Forms;

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

        public string State => _Member.State;

        public string District => _Member.District;

        public ObservableCollection<BillViewModel> Bills { get; set; }
        public ObservableCollection<VoteViewModel> Votes { get; set; }
        #endregion

        #region Private Properties
        private Member _Member {get;set;}

        #endregion

        #region Ctor

        public MemberViewModel(Member mem)
        {
            _Member = mem;
            Bills = new ObservableCollection<BillViewModel>();
            Votes = new ObservableCollection<VoteViewModel>();
            InitBaseVM();
        }

        public MemberViewModel()
        {
            //Template data for XAML
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
            Bills = new ObservableCollection<BillViewModel>();
            Votes = new ObservableCollection<VoteViewModel>();
            InitBaseVM();
        }
        #endregion


        #region Public Methods
        public override void OnShow(NavPage Page)
        {
            base.OnShow(Page);
            Update();
        }
        #endregion Public Methods

        #region Private Methods

        private async void Update()
        {
            var bills = await IoC.API.BillsByMemberAsync(_Member.ID);

            if (bills.Any())
            {
                Bills.Clear();
                for (var i = 0; i < 5; i++)
                {
                    Bills.Add(new BillViewModel(bills[i]));
                }
            }

            var votes = await IoC.API.VotesByMemberAsync(_Member.ID);

            if (votes.Any())
            {
                Votes.Clear();
                for (var i = 0; i < 5; i++)
                {
                    Votes.Add(new VoteViewModel(votes[i]));
                }
            }
        }


        #endregion
    }
}
