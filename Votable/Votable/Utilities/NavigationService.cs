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
        public async Task NavigateToMember(BaseViewModel vm, NavPage P = NavPage.Unknown)
        {
            Type T = null;
            if (P == NavPage.Unknown)
            {
                if (vm is MemberViewModel)
                {
                    T = typeof(MemberDetailPage);
                    P = NavPage.MemberDetail;
                }
                else if (vm is BillViewModel)
                {
                    T = typeof(BillDetailPage);
                    P = NavPage.BillDetail;
                }
            }
            else
            {
                switch(P)
                {
                    case (NavPage.UserInterest):
                        T = typeof(UserInterestPage);
                        break;
                    case (NavPage.BillList):
                        T = typeof(BillListPage);
                        break;
                    case (NavPage.MemberList):
                        T = typeof(MemberListPage);
                        break;
                }
            }
            if (T != null)
            {
                var obj = T.GetConstructor(new Type[] { }).Invoke(new object[] { });
                if (obj is Page page)
                {
                    page.BindingContext = vm;
                    await (App.Current as App).Navigation.PushAsync(page);
                    
                }
            }

        }

        public static NavPage AsNavPage(Page P)
        {
            if(P is HomePage)
            {
                return NavPage.Home;
            }
            if (P is BillListPage)
            {
                return NavPage.BillList;
            }
            if (P is BillDetailPage)
            {
                return NavPage.BillDetail;
            }
            if (P is MemberDetailPage)
            {
                return NavPage.MemberDetail;
            }
            if (P is MemberListPage)
            {
                return NavPage.MemberList;
            }
            if (P is UserInterestPage)
            {
                return NavPage.UserInterest;
            }
            throw new NotImplementedException(P.GetType() + " is not supported as NavPage");
        }

        

    }

    public enum NavPage
    {
        Unknown,
        Home,
        MemberList,
        MemberDetail,
        BillList,
        BillDetail,
        UserInterest,
    }
}
