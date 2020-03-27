using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace KagerApp.common
{
    class RelayCommand:ICommand
    {
        private Action _actionToDo;

        public RelayCommand(Action actionToDo)
        {
            _actionToDo = actionToDo;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _actionToDo();
        }

        public event EventHandler CanExecuteChanged;
    }
}
