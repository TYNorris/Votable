using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Votable.Models;
using Votable.Utilities;

namespace Votable.ViewModels
{
    public class BillListViewModel: BaseViewModel
    {
        private ObservableCollection<BillViewModel> _bills { get; set; }
        public ObservableCollection<BillViewModel> Bills
        {
            get => _bills;
            set
            {
                if (_bills != null && _bills.Equals(value))
                    return;
                _bills = value;
                OnPropertyChanged(nameof(Bills));
            }
        }

        public override void OnShow(NavPage Page)
        {
            base.OnShow(Page);
            Bills = IoC.RecentBills;
        }

    }
}
