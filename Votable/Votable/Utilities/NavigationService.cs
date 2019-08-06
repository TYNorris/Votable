using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Votable.ViewModels;
using Votable.Views;
using Xamarin.Forms;

namespace Votable.Utilities
{
    class NavigationService
    {
        

        public NavigationService()
        {

        }
        public async Task NavigateToMember(MemberViewModel vm)
        {
            
                await (App.Current as App).Navigation.PushAsync(new MemberDetailPage()
                {
                    BindingContext = vm
                });
            
        }

    }
}
