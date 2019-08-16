using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Votable.Pages;
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
        public async Task NavigateToMember(BaseViewModel vm)
        {
            if (vm is MemberViewModel member)
            {
                await (App.Current as App).Navigation.PushAsync(new MemberDetailPage()
                {
                    BindingContext = member
                });
            }
            else if(vm is BillViewModel bill)
            {
                await (App.Current as App).Navigation.PushAsync(new BillDetailPage()
                {
                    BindingContext = bill
                });
            }
        }

    }
}
