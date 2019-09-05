using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Votable.Models;
using Votable.Utilities;
using Votable.ViewModels;

namespace Votable
{
    public static class IoC
    {
        public static CongressAPI API;

        public static ManualResetEvent Ready;

        private static ISimpleIoc Container;
        private static bool IsInitialized = false;

        public static void Init()
        {
            if (!IsInitialized)
            {
                Container = SimpleIoc.Default;
                Ready = new ManualResetEvent(false);
                Senators = new ObservableCollection<MemberViewModel>();
                HouseMembers = new ObservableCollection<MemberViewModel>();
                RecentBills = new ObservableCollection<BillViewModel>();
                Container.Register<FileService>(true);
                Container.Register<NavigationService>(true);
                Container.Register<CongressAPI>(true);
                Container.Register<UserViewModel>(true);
                API = Get<CongressAPI>();
                API.PropertyChanged += APIDataChanged;
                IsInitialized = true;
            }

        }

        public static void OnResume()
        {
            Get<CongressAPI>().OnResume();
        }

        public static T Get<T>() => Container.GetInstance<T>();
                    
   
        /// <summary>
        /// Update data from API when available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void APIDataChanged(object sender, PropertyChangedEventArgs e)
        {
            //Update current list of senators 
            if (e.PropertyName.Equals(nameof(CongressAPI.Senators)))
            {
                Senators.Clear();
                foreach(var s in API.Senators)
                {
                    Senators.Add(new MemberViewModel(s));
                }
                Ready.Set();
            }
            //Update current list of house members 
            if (e.PropertyName.Equals(nameof(CongressAPI.HouseMembers)))
            {
                HouseMembers.Clear();
                foreach (var s in API.HouseMembers)
                {
                    HouseMembers.Add(new MemberViewModel(s));
                }
                Ready.Set();
            }
            //Update current list of bills
            if (e.PropertyName.Equals(nameof(CongressAPI.AllBills)))
            {
                RecentBills.Clear();
                foreach (var b in API.AllBills)
                {
                    RecentBills.Add(new BillViewModel(b));
                }
            }
        }

        public static async  Task<IEnumerable<MemberViewModel>> SenatorsByAddress(string address)
        {
            var state = await API.StateCodeByAddress(address);
            if (!string.IsNullOrEmpty(state))
            {
                return Senators.Where(s => s.State.Equals(state, StringComparison.OrdinalIgnoreCase));
            }
            else return new List<MemberViewModel>();    
        }

        public static async Task<MemberViewModel> HouseMemberByAddress(string address)
        {
            var district = await API.HouseDistrictByAddress(address);
            if (!string.IsNullOrEmpty(district))
            {
                var split = district.Split('/');
                return HouseMembers.First(m => m.State.Equals(split.First(), StringComparison.OrdinalIgnoreCase) &&
                                                m.District.Equals(split.Last()));
            }
            else return null;
        }


        public static ObservableCollection<MemberViewModel> Senators { get; private set; }
        public static ObservableCollection<MemberViewModel> HouseMembers { get; private set; }
        public static ObservableCollection<BillViewModel> RecentBills { get; private set; }
    }
}
