
using System;
using System.Collections.Generic;
using System.Text;
using Votable.Utilities;
using Xamarin.Forms;

namespace Votable
{
    public class BasePage: ContentPage
    {

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is BaseViewModel vm)
            {
                vm.OnShow(NavigationService.AsNavPage(this));
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is BaseViewModel vm)
            {
                vm.OnHide(NavigationService.AsNavPage(this));
            }
        }
    }
}
