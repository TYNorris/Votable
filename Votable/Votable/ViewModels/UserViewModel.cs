using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Votable.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        private string _address;
        public string Address {
            get => _address;
            set
            {
                if (_address == value)
                    return;
                _address = value;
                OnPropertyChanged(Address);
                UpdateAddress();
            }
        }

        public ObservableCollection<MemberViewModel> Reps {get;set;}

        public UserViewModel()
        {
            Reps = new ObservableCollection<MemberViewModel>();
            Address = "02134";
            Init();
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
    }
}
