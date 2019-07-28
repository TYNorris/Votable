using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Votable
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

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

        private RelayCommand _showCommand { get; set; }

        private RelayCommand _hideCommand { get; set; }
        public RelayCommand OnShowCommand
        {
            get
            {
                if (_showCommand == null)
                    _showCommand = new RelayCommand(new Action(OnShow));
                return _showCommand;

            }
        }
        public RelayCommand OnHideCommand
        {
            get
            {
                if (_hideCommand == null)
                    _hideCommand = new RelayCommand(new Action(OnHide));
                return _hideCommand;

            }
        }

        public virtual void OnShow() { }
        public virtual void OnHide() { }
    }
}
