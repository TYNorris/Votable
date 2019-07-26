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
            catch(InvalidOperationException)
            {

            }
        }
    }
}
