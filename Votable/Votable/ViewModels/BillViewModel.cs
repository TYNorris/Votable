using System;
using System.Collections.Generic;
using System.Text;
using Votable.Models;

namespace Votable.ViewModels
{
    public class BillViewModel
    {
        #region Public Properties
        public string Number => _Bill.Number;

        public string Title => _Bill.Title;

        public string ShortTitle => _Bill.ShortTitle;

        public string Subtitle => _Bill.Number + " - " + _Bill.Subject;

        #endregion Public Properties

        #region Private Properties
        private Bill _Bill {get;set;}
        #endregion Private Properties

        #region Constructor
        public BillViewModel(Bill bill)
        {
            _Bill = bill;
        }
        #endregion Constructo
    }
}
