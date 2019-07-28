using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Votable
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action Action { get; set; }

        public RelayCommand(Action act)
        {
            Action = act;
        }

        public bool CanExecute(object parameter)
        {
                return true;
        }

        public void Execute(object parameter)
        {
            Action.Invoke();
        }
    }
}
