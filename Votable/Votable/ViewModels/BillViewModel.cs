using System;
using System.Collections.Generic;
using System.Text;
using Votable.Models;
using Votable.Utilities;

namespace Votable.ViewModels
{
    public class BillViewModel: BaseViewModel
    {
        #region Public Properties
        public string Number => _Bill.Number;

        public string Title => _Bill.Title;

        public string ShortTitle => _Bill.ShortTitle;
        public string Summary => String.IsNullOrEmpty(_Bill.Summary)? _Bill.Title : _Bill.Summary;

        public string Updated => _Bill.DetailsAdded ? "Up to date" : "Loading...";

        public string Subtitle => _Bill.Number + " - " + _Bill.Subject;

        public List<BillSubject> Subjects => _Bill.Subjects;

        #endregion Public Properties

        #region Private Properties
        private Bill _Bill {get;set;}

        #endregion Private Properties

        #region Constructor
        public BillViewModel(Bill bill)
        {
            _Bill = bill;
            InitBaseVM();
        }

        //XAML Constructor
        public BillViewModel()
        {
            _Bill = new Bill()
            {
                Title = "A Bill for Display the User Interface of Software Applications",
                Summary = "This bill is used as a display template for various components of software applications. Lorem ipsum and many more....",
                ShortSummary = "This bill is used as a display template for various components of software applications",
                IntroducedDate = DateTime.Today,
                HousePassageDate = DateTime.Today
            };
            InitBaseVM();
        }
        #endregion Constructo

        #region Public Methods
        public async void AddDetails()
        {
            if(!_Bill.DetailsAdded)
            {
                LockProperty(nameof(_Bill));
                var details = await IoC.API.BillByIDAsync(_Bill.ID);
                _Bill.AddEmptyProperties<Bill>(details);
                var subjects = await IoC.API.BillSubjectsByIDAsync(_Bill.ID);
                _Bill.AddEmptyProperties<Bill>(subjects);
                _Bill.DetailsAdded = true;
                UnlockProperty(nameof(_Bill));
                NotifyProperties();
            }
        }

        public void NotifyProperties()
        {
            OnPropertyChanged(nameof(Summary));
            OnPropertyChanged(nameof(Updated));
            OnPropertyChanged(nameof(ShortTitle));
            OnPropertyChanged(nameof(Subjects));
        }

        #endregion

        #region Override Methods
        public override void OnShow(NavPage Page)
        {
            base.OnShow(Page);
            AddDetails();
        }
        #endregion
    }
}
