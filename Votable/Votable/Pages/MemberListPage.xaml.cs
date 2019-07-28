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
    public partial class MemberListPage : ContentPage
    {
        public MemberListPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(BindingContext is BaseViewModel vm)
            {
                vm.OnShow();
            }
        }


        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new MemberDetailPage
                {
                    BindingContext = e.SelectedItem as Member
                });
            }
        }

    }
}