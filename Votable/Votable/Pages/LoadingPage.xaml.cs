using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votable.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Votable.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {

        }
    }
}