using System;
using System.Collections.Generic;

namespace Toes
{
	public class BaseGameLogic
	{
		public const int NumberOfColumns = 3;
		public const int NumberOfRows = 3;
		private Player _currentPlayer;

		public BaseGameLogic()
		{
			Player1 = new Player { Name = "Player 1", Icon = "X" };
			Player2 = new Player { Name = "Player 2", Icon = "O" };

			for (int i = 0; i < NumberOfColumns; i++)
			{
				var rowMap = new Dictionary<int, Player>();

				for (int j = 0; j < NumberOfRows; j++)
				{
					rowMap[j] = null;
				}

				PlayerMap[i] = rowMap;
			}
		}

		public Dictionary<int, Dictionary<int, Player>> PlayerMap { get; } = new Dictionary<int, Dictionary<int, Player>>();

		public Player CurrentPlayer
		{
			get => _currentPlayer?? Player1;
			set
			{
				if(_currentPlayer == value)
					return;

				_currentPlayer = value;
				CurrentPlayerChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Player Player1 { get; set; }
		public Player Player2 { get; set; }

		public event EventHandler CurrentPlayerChanged;
		public event EventHandler MapChanged;

		public void ClaimBoxForCurrentPlayer(int xPosition, int yPosition)
		{
			if (PlayerMap[xPosition][yPosition] != null && PlayerMap[xPosition][yPosition] != CurrentPlayer)
				throw new InvalidOperationException("Cheater!");

			PlayerMap[xPosition][yPosition] = CurrentPlayer;
			CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;

			MapChanged?.Invoke(this, EventArgs.Empty);

			CheckWinConditions();
		}

		private void CheckWinConditions()
		{

		}
	}
}