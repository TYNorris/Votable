
using System;
using System.Collections.Generic;
using System.Text;
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
                vm.OnShow();
            }
        }
    }
}
