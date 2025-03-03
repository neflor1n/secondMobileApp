using System;
using Microsoft.Maui.Controls;

namespace secondMobileApp
{
    public partial class TickTackToe : ContentPage
    {
        private string currentPlayer = "X";  // Текущий игрок (X или O)
        private string[,] board = new string[3, 3];  // Игровое поле
        private bool gameOver = false;

        // Переменные для хранения цветов для X и O
        private Color XColor = Colors.Red;  // Цвет для X
        private Color OColor = Colors.Blue; // Цвет для O

        public TickTackToe()
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
                button.TextColor = currentPlayer == "X" ? XColor : OColor;  // Установка цвета текста
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
                    btn.TextColor = Colors.Black; 
                }
            }
        }

        // сохранения цветов X и O
        private void OnSaveColorsClicked(object sender, EventArgs e)
        {
            string xColorHex = XColorEntry.Text;
            if (Color.TryParse(xColorHex, out Color parsedXColor))
            {
                XColor = parsedXColor;
            }
            else
            {
                DisplayAlert("Viga", "Kehtetu HEX-kood X-i jaoks", "OK");
            }

            string oColorHex = OColorEntry.Text;
            if (Color.TryParse(oColorHex, out Color parsedOColor))
            {
                OColor = parsedOColor;
            }
            else
            {
                DisplayAlert("Viga", "Vale HEX-kood O jaoks", "OK");
            }


            UpdateBoardColors();
        }

        // Метод для сброса цветов X и O на стандартные
        private void OnResetColorsClicked(object sender, EventArgs e)
        {
            XColor = Colors.Red;
            OColor = Colors.Blue;

            XColorEntry.Text = string.Empty;
            OColorEntry.Text = string.Empty;

            Console.WriteLine($"Värv X lähtesta: {XColor}, Värv O lähtesta: {OColor}");

            // Перекрашивание всех уже поставленных символов
            UpdateBoardColors();
        }

        // обновления цвета на всех клетках
        private void UpdateBoardColors()
        {
            foreach (var button in GameBoard.Children)
            {
                if (button is Button btn)
                {
                    if (btn.Text == "X")
                    {
                        btn.TextColor = XColor; 
                    }
                    else if (btn.Text == "O")
                    {
                        btn.TextColor = OColor;
                    }
                }
            }
        }

        private async void OnSelectColorClicked(object sender, EventArgs e)
        {
            string selectedColor = await DisplayActionSheet("Select a Color", "Cancel", null,
                "Red (#FF0000)", "Green (#00FF00)", "Blue (#0000FF)", "Yellow (#FFFF00)", "Cyan (#00FFFF)", "Magenta (#FF00FF)");

            if (selectedColor != "Cancel")
            {
                string hexCode = selectedColor switch
                {
                    "Red (#FF0000)" => "#FF0000",
                    "Green (#00FF00)" => "#00FF00",
                    "Blue (#0000FF)" => "#0000FF",
                    "Yellow (#FFFF00)" => "#FFFF00",
                    "Cyan (#00FFFF)" => "#00FFFF",
                    "Magenta (#FF00FF)" => "#FF00FF",
                    _ => "#FFFFFF"
                };

                await Clipboard.SetTextAsync(hexCode);

                string copiedText = await Clipboard.GetTextAsync();

                if (!string.IsNullOrEmpty(copiedText))
                {
                    await DisplayAlert("Värv Copid", $"{selectedColor} on kopeeritud lõikelauale", "OK");
                }
                else
                {
                    await DisplayAlert("Viga", "Arhiveeritud kopeeritud värviline lõikelaud.", "OK");
                }
            }
        }

    }
}
