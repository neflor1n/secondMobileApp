using System;
using Microsoft.Maui.Controls;

namespace secondMobileApp
{
    public partial class TickTackToe : ContentPage
    {
        private string currentPlayer = "X";  // Текущий игрок (X или O)
        private string[,] board = new string[3, 3];  // Игровое поле
        private bool gameOver = false;

        public TickTackToe(int k)
        {
            InitializeComponent();
        }

        // Метод обработки нажатия на клетку
        private void OnCellClicked(object sender, EventArgs e)
        {
            if (gameOver) return;  // Если игра завершена, ничего не делаем

            var button = (Button)sender;
            var row = Grid.GetRow(button);
            var col = Grid.GetColumn(button);

            if (string.IsNullOrEmpty(button.Text))  // Проверка, что клетка не занята
            {
                button.Text = currentPlayer;
                board[row, col] = currentPlayer;

                if (CheckWin())  // Проверка на победу
                {
                    WinnerLabel.Text = $"{currentPlayer} Vonn!";
                    gameOver = true;
                    RestartGrid.IsVisible = true;  
                }
                else if (IsBoardFull())  // Проверка на ничью
                {
                    WinnerLabel.Text = "Viik!";
                    gameOver = true;
                    RestartGrid.IsVisible = true;  
                }
                else
                {
                    currentPlayer = currentPlayer == "X" ? "O" : "X";  
                }
            }
        }

        // Проверка на победителя
        private bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) return true;
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer) return true;
            }

            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) return true;
            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer) return true;

            return false;
        }

        // Проверка на ничью
        private bool IsBoardFull()
        {
            foreach (var cell in board)
            {
                if (string.IsNullOrEmpty(cell)) return false;
            }
            return true;
        }

        // Сброс игры
        private void OnRestartClicked(object sender, EventArgs e)
        {
            currentPlayer = "X";
            gameOver = false;
            WinnerLabel.Text = string.Empty;
            RestartGrid.IsVisible = false;  
            Array.Clear(board, 0, board.Length);

            foreach (var button in GameBoard.Children)
            {
                if (button is Button btn)
                {
                    btn.Text = string.Empty;
                }
            }
        }
    }
}
