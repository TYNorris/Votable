using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Votable.Models;
using Votable.ViewModels;

namespace Votable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillListPage : BasePage
    {
        public BillListPage()
        {
            InitializeComponent();
        }


    }
}