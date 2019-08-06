using System;
using System.Collections.Generic;
using System.Text;
using Votable.Models;

namespace Votable.ViewModels
{
    public class VoteViewModel: BaseViewModel
    {
        private Vote _vote;

        public string Title => _vote.Bill.Title;

        public string SubTitle => _vote.Position + " - " + _vote.Result + " - " + _vote.Date;

        public VoteViewModel(Vote v)
        {
            _vote = v;
        }
    }
}
