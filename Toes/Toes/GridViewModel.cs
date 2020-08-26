using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Toes
{
	public class GridViewModel: INotifyPropertyChanged
	{
		private readonly BaseGameLogic _logic;
		private Player _currentPlayer;

		public GridViewModel()
		{
			_logic = new BaseGameLogic();
			_logic.CurrentPlayerChanged += Logic_CurrentPlayerChanged;
			CurrentPlayer = _logic.CurrentPlayer;

			Grid = new ObservableCollection<ObservableCollection<BoxViewModel>>();

			for (int i = 0; i < BaseGameLogic.NumberOfColumns; i++)
			{
				ObservableCollection<BoxViewModel> columnList = new ObservableCollection<BoxViewModel>();

				for (int j = 0; j < BaseGameLogic.NumberOfRows; j++)
				{
					columnList.Add(new BoxViewModel(i, j, _logic));
				}

				Grid.Add(columnList);
			}
		}

		public ObservableCollection<ObservableCollection<BoxViewModel>> Grid { get; }

		public Player CurrentPlayer
		{
			get => _currentPlayer;
			set
			{
				if (_currentPlayer == value)
					return;

				_currentPlayer = value;
				NotifyPropertyChanged();
			}
		}

		private void Logic_CurrentPlayerChanged(object sender, EventArgs e)
		{
			CurrentPlayer = _logic.CurrentPlayer;
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
