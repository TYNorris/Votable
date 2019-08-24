using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votable.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Votable.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BasePage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = IoC.Get<UserViewModel>();
        }
    }
}