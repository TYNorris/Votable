using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Votable.Models;
using Votable.ViewModels;

namespace Votable
{
    public static class IoC
    {
        public static CongressAPI API;

        public static ManualResetEvent Ready;
        public static void Init()
        {
            Ready = new ManualResetEvent(false);
            API = new CongressAPI();
            Members = new ObservableCollection<MemberViewModel>();
            API.PropertyChanged += APIDataChanged;
        }

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
                Members.Clear();
                foreach(var s in API.Senators)
                {
                    Members.Add(new MemberViewModel(s));
                }
                Ready.Set();
            }
        }

        public static ObservableCollection<MemberViewModel> Members { get; private set; }
    }
}
