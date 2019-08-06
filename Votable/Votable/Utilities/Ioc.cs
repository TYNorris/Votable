﻿using GalaSoft.MvvmLight.Ioc;
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
        
        public static void Init()
        {
            Container = SimpleIoc.Default;
            Ready = new ManualResetEvent(false);
            Senators = new ObservableCollection<MemberViewModel>();
            Container.Register<NavigationService>(true);
            Container.Register<CongressAPI>(true);
            API = Get<CongressAPI>();
            API.PropertyChanged += APIDataChanged;
        }

        public static T Get<T>() => Container.GetInstance<T>();
                    
   
        /// <summary>
        /// Update data from API when available
        /// </summary>
        /// <param name="sendre"></param>
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

        public static ObservableCollection<MemberViewModel> Senators { get; private set; }
    }
}
