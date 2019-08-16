using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Votable.Utilities;

namespace Votable
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };


        public List<string> UpdatingProperties { get; private set; } = new List<string>();

        /// <summary>
        /// Called when page appears
        /// </summary>
        public virtual void OnShow() { }

        /// <summary>
        /// Callded when page disappears
        /// </summary>
        public virtual void OnHide() { }


        #region Public Methods
        internal async void NavigateHere()
        {
            await IoC.Get<NavigationService>().NavigateToMember(this);
        }
        public void LockProperty(string property)
        {
            if(!UpdatingProperties.Contains(property))
            {
                UpdatingProperties.Add(property);
            }
        }

        public void UnlockProperty(string property)
        {
            if (UpdatingProperties.Contains(property))
            {
                UpdatingProperties.Remove(property);
            }
        }

        public bool IsPropertyFree(string property) => !UpdatingProperties.Contains(property);


        public void Init()
        {
            SelectedCommand = new RelayCommand(NavigateHere);
        }

        /// <summary>
        /// Trigger property changed
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            try
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
            catch (InvalidOperationException)
            {

            }
        }

        #endregion

        #region Commands

        public RelayCommand SelectedCommand { get; set; }
        #endregion
    }
}
