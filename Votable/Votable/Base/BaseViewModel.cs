using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Votable
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

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


        /// <summary>
        /// Called when page appears
        /// </summary>
        public virtual void OnShow() { }

        /// <summary>
        /// Callded when page disappears
        /// </summary>
        public virtual void OnHide() { }
    }
}
