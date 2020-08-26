using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Toes
{
	public class DelegateCommand : ICommand
	{
		private readonly Action _action;
		private readonly bool _ranAsTask;

		public DelegateCommand(Action action)
			: this(action, true)
		{ }

		public DelegateCommand(Action action, bool runAsTask)
		{
			_action = action;
			_ranAsTask = runAsTask;
		}

		public virtual void Execute(object parameter)
		{
			if (_ranAsTask)
				Task.Factory.StartNew(_action);
			else
				_action.Invoke();
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;
	}
}