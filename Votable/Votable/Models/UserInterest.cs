using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Votable.Models
{
    public class UserInterest : INotifyPropertyChanged
    {
        #region Public Properties
        public string Title
        {
            get => _title;
            set
            {
                if (_title != null && _title.Equals(value))
                    return;
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled == value)
                    return;
                _enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }
        #endregion

        #region Constructor
        public UserInterest(string title, bool enabled)
        {
            Title = title;
            Enabled = enabled;
        }
        #endregion

        #region Fields
        private string _title = "";

        private bool _enabled = false;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        public void OnPropertyChanged(string name)
        {
            try
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
            catch (Exception)
            {

            }
        }
    }
}
