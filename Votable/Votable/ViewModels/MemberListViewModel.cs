using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Votable.Models;
using Votable.Utilities;

namespace Votable.ViewModels
{
    public class MemberListViewModel: BaseViewModel
    {
        private ObservableCollection<MemberViewModel> _members { get; set; }
        public ObservableCollection<MemberViewModel> Members
        {
            get => _members;
            set
            {
                if (_members != null && _members.Equals(value))
                    return;
                _members = value;
                OnPropertyChanged(nameof(Members));
            }
        }

        public override void OnShow(NavPage Page)
        {
            base.OnShow(Page);
            Members = IoC.Senators;
        }

    }
}
