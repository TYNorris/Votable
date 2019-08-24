using GalaSoft.MvvmLight.Ioc;
using System;
using System.IO;
using System.Threading.Tasks;
using Votable.Pages;
using Votable.Utilities;
using Votable.ViewModels;
using Votable.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Votable
{
    public partial class App : Application
    {
        public static string FolderPath { get; internal set; }

        public INavigation Navigation => (MainPage as MainPage).Detail.Navigation;
        public App()
        {
            InitializeComponent();
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new LoadingPage();
        }


        protected override void OnStart()
        {
            IoC.Init();
            // Handle when your app starts
            Task.Run(() =>
            {
                IoC.Ready.WaitOne(10000);
                MainPage = new MainPage();
            });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Save();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

        }

        private void Save()
        {
            IoC.Get<UserViewModel>().Save();
        }

    }

}
