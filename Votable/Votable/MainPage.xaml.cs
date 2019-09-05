using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Votable.Models;
using Votable.Utilities;

namespace Votable
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            masterPage.navList.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if( e.SelectedItem is NavMenuItem item)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.Target));
                masterPage.navList.SelectedItem = null;
                IsPresented = false;
                if(item.ViewModel != null)
                {
                    Detail.BindingContext = item.ViewModel;
                }
            }
        }

    }
}