using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Votable.Base
{
    public class ParameterComand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<T> Action { get; set; }

        public ParameterComand(Action<T> act)
        {
            Action = act;
        }

        public bool CanExecute(object parameter)
        {
                return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is T param)
                Action.Invoke(param);
            else
                throw new InvalidOperationException("Incorrect type: " + parameter.GetType().Name + " but expected " + typeof(T).Name); 
        }
    }
}
