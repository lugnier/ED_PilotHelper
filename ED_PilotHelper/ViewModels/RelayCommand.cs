using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

// ReSharper disable once CheckNamespace
namespace Utils
{
    /// <summary>
    /// classe d'implémentation de ICommand
    /// reprise de la classe MSDN
    /// finalisation avec la gestion des paramètres
    /// </summary>
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action _Execute;
        Action<object> _ExecuteWithParam;

        Func<Boolean> _CanExecute;
        Func<object, Boolean> _CanExecuteWithParam;


        public RelayCommand(Action m1, Func<Boolean> m2)
        {
            _Execute = m1;
            _CanExecute = m2;
        }


        public RelayCommand(Action m1) : this(m1, () => true)
        {
        }

        public RelayCommand(Action<object> m1, Func<object, Boolean> m2 = null)
        {
            _ExecuteWithParam = m1;
            if (m2 == null)
            {
                _CanExecuteWithParam = (x) => true;
            }
            else
            {
                _CanExecuteWithParam = m2;
            }
        }
        public bool CanExecute(object parameter)
        {
            if (_CanExecute != null && parameter == null)
            {

                return _CanExecute();
            }

            if (_CanExecuteWithParam != null && parameter != null)
            {

                return _CanExecuteWithParam(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (_Execute != null && parameter == null)
            {
                _Execute();
                return;
            }

            if (_ExecuteWithParam != null && parameter != null)
            {
                _ExecuteWithParam(parameter);
            }


        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
