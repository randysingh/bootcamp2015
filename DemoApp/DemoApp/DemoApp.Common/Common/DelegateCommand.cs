using System;

namespace DemoApp.Common.Common
{
    public class DelegateCommand : System.Windows.Input.ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute)
            : this(execute, () => true) { /* empty */ }

        public DelegateCommand(Action execute, Func<bool> canexecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canexecute;
        }

        public bool CanExecute(object p)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object p)
        {
            if (CanExecute(null))
                _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}