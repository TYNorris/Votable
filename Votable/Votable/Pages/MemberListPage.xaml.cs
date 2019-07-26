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

            var Members = new List<MemberViewModel>();

            for(var i = 0; i< 10; i++)
            {
                Members.Add(new MemberViewModel(new Member()
                {
                    FirstName = "Test",
                    LastName = i.ToString(),
                    Party = i%2 ==0 ? "Democrat": "Republican"
                }));
            }

            listView.ItemsSource = Members.ToList();
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