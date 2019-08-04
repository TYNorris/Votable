using System;
using System.IO;
using System.Threading.Tasks;
using Votable.Pages;
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
            MainPage = new LoadingPage();
        }

        protected override void OnStart()
        {
            IoC.Init();
            OnResume();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            MainPage = new LoadingPage();
            Task.Run(() =>
            {
                IoC.Ready.WaitOne(10000);
                MainPage = new NavigationPage(new MemberListPage());
            });
        }

    }

}
