﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Votable.Models;
using Votable.Pages;
using Votable.Views;

namespace Votable.ViewModels
{
    public class MainMenuViewModel
    {
        public ObservableCollection<NavMenuItem> Pages { get; set; }

        public MainMenuViewModel()
        {
            Pages = new ObservableCollection<NavMenuItem>()
            {
                new NavMenuItem("Home", "temp.png", typeof(HomePage)),
                new NavMenuItem("Senators", "temp.png", typeof(MemberListPage)),
            };

        }
    }
}
