using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Votable.Models;
using Votable.Utilities;

namespace Votable.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        private User _user;
        [JsonProperty]
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

        [JsonProperty]
        public ObservableCollection<MemberViewModel> Reps {get;set;}

        public UserViewModel()
        {
            Reps = new ObservableCollection<MemberViewModel>();
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

        public override void OnShow()
        {
            base.OnShow();
            UpdateAddress();
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
    }
}
