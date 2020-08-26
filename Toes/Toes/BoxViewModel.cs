using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Toes
{
	public class BoxViewModel : INotifyPropertyChanged
	{
		private readonly BaseGameLogic _logic;
		private readonly Tuple<int, int> _position;
		private Player _owningPlayer;
		public BoxViewModel(int xPosition, int yPosition, BaseGameLogic logic)
		{
			_logic = logic;
			_position = new Tuple<int, int>(xPosition, yPosition);
			logic.MapChanged += Logic_MapChanged;
			TakeBox = new DelegateCommand(TakeBoxInternal);
		}

		public ICommand TakeBox { get; }

		public Player OwningPlayer
		{
			get => _owningPlayer;
			set
			{
				if (_owningPlayer == value)
					return;

				_owningPlayer = value;
				NotifyPropertyChanged();
			}
		}
		
		private void TakeBoxInternal()
		{
			_logic.ClaimBoxForCurrentPlayer(_position.Item1, _position.Item2);
		}

		private void Logic_MapChanged(object sender, EventArgs e)
		{
			OwningPlayer = _logic.PlayerMap[_position.Item1][_position.Item2];
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged([CallerMemberName] string caller = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
		}

		#endregion
	}
}