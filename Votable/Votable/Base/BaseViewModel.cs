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

        public List<string> UpdatingProperties { get; private set; } = new List<string>();

        /// <summary>
        /// Called when page appears
        /// </summary>
        public virtual void OnShow() { }

        /// <summary>
        /// Callded when page disappears
        /// </summary>
        public virtual void OnHide() { }

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
    }
}
