using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Votable.Models;
using Votable.Utilities;

namespace Votable.ViewModels
{
    public class MemberListViewModel: BaseViewModel
    {

        private bool _hasSenate = false;
        private bool _hasHouse = false;
       
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

        public MemberListViewModel()
        {
            Members = new ObservableCollection<MemberViewModel>();
            _hasSenate = true;
        }

        public MemberListViewModel(bool hasSenate, bool hasHouse)
        {
            Members = new ObservableCollection<MemberViewModel>();
            _hasSenate = hasSenate;
            _hasHouse = hasHouse;
        }
        public override void OnShow(NavPage Page)
        {
            base.OnShow(Page);
            IEnumerable<MemberViewModel> mems = new List<MemberViewModel>();
            if (_hasSenate)
                mems = mems.Concat(IoC.Senators);
            if (_hasHouse)
                mems = mems.Concat(IoC.HouseMembers);
            Members = new ObservableCollection<MemberViewModel>(mems);

        }



    }
}
