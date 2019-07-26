using System;
using System.IO;
using Votable.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Votable
{
    public partial class App : Application
    {
        public static string FolderPath { get; internal set; }

        public App()
        {
            InitializeComponent();
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new NavigationPage(new MemberListPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
