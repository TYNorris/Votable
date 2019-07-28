﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Votable.Models;
using Votable.ViewModels;

namespace Votable
{
    public static class IoC
    {
        private static CongressAPI api;
        public static void Init()
        {
            api = new CongressAPI();
            Members = new ObservableCollection<MemberViewModel>();
            api.PropertyChanged += APIDataChanged;
        }

        public static void APIDataChanged(object sendre, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(CongressAPI.Senators)))
            {
                Members.Clear();
                foreach(var s in api.Senators)
                {
                    Members.Add(new MemberViewModel(s));
                }
            }
        }

        public static ObservableCollection<MemberViewModel> Members { get; private set; }
    }
}