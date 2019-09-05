using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Votable.Models
{
    public class NavMenuItem
    {
        public string Icon { get; set; }

        public string Title { get; set; }

        public Type Target { get; set; }

        public BaseViewModel ViewModel { get; set; }

        public NavMenuItem(string title, string icon, Type target, BaseViewModel vm = null)
        {
            Title = title;
            Icon = icon;
            Target = target;
            ViewModel = vm;
        }

    }
}
