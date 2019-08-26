using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Votable.Models;
using Votable.Utilities;
using Votable.Pages;
using System.Collections.Specialized;

namespace Votable.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        #region Public Properties
        public string Address {
            get => _user.Address;
            set
            {
                if (_user.Address == value)
                    return;
                _user.Address = value;
                OnPropertyChanged(Address);
                UpdateAddress();
            }
        }

        public ObservableCollection<MemberViewModel> Reps {get;set;}

        public ObservableCollection<UserInterest> Interests { get; set; }

        public ObservableCollection<BillViewModel> RelevantBills { get; set; }

        #endregion

        #region Ctor
        public UserViewModel()
        {
            Reps = new ObservableCollection<MemberViewModel>();
            Interests = new ObservableCollection<UserInterest>();
            RelevantBills = new ObservableCollection<BillViewModel>();
            NavigateToInterests = new RelayCommand(ChooseInterests);
            var userstring = IoC.Get<FileService>().ReadFile(User.UserFile);
            if (String.IsNullOrEmpty(userstring))
            {
                _user = new User();
            }
            else
            {
                _user = new User(userstring);
            }
            InitBaseVM();
        }

        private async void UpdateRelevantBills()
        {
            RelevantBills.Clear();
            foreach(var i in Interests)
            {
                if(i.Enabled)
                {
                    var bills = await IoC.API.BillsBySubjectAsync(i.Title);
                    if (bills != null)
                    {
                        foreach (var b in bills)
                        {
                            if (RelevantBills.Count < 5)
                            {
                                RelevantBills.Add(new BillViewModel(b));
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Fields

        private User _user;
        #endregion

        #region Commands
        public RelayCommand NavigateToInterests { get; set; }
        #endregion

        #region Public Methods
        public override void OnShow(NavPage Page)
        {
            base.OnShow(Page);
            UpdateAddress();
        }

        public override void OnHide(NavPage Page)
        {
            base.OnHide(Page);
            if(Page == NavPage.UserInterest)
            {
                UpdateRelevantBills();
            }
        }

        public async void UpdateAddress()
        {
            if (Reps != null && IsPropertyFree(nameof(Address)))
            {
                LockProperty(nameof(Address));
                try
                {
                    Reps.Clear();
                    var sens = await IoC.SenatorsByAddress(Address);
                    foreach (var s in sens)
                    {
                        Reps.Add(s);
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    UnlockProperty(nameof(Address));
                }
            }
        }

        public void Save() => _user.Save();
        #endregion

        #region Private Methods 

        private async void ChooseInterests()
        {
            InitInterests();
            await IoC.Get<NavigationService>().NavigateToMember(this, NavPage.UserInterest);
        }
        private void InitInterests()
        {
            if (Interests.Count == 0)
            {
                Interests.Add(new UserInterest("Voting", true));
                Interests.Add(new UserInterest("Climate", false));
                Interests.Add(new UserInterest("Defense", false));
                Interests.Add(new UserInterest("Education", false));
                Interests.Add(new UserInterest("Budget", false));
            }
        }
        #endregion
    }
}
