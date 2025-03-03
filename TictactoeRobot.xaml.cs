namespace secondMobileApp;

public partial class TictactoeRobot : ContentPage
{
    private string currentPlayer = "X";
    private string[,] board = new string[3, 3];
    private bool gameOver = false;
    private Random randChoose = new Random();

    private Color XColor = Colors.Red;
    private Color OColor = Colors.Blue;

    public TictactoeRobot()
    {
        InitializeComponent();
        // После инициализации компонента устанавливаем цвета для X и O
        UpdateBoardColors();
    }

    private void OnCellClicked(object sender, EventArgs e)
    {
        if (gameOver) return;
        Button clickedButton = (Button)sender;

        if (!string.IsNullOrEmpty(clickedButton.Text)) return;

        int row = Grid.GetRow(clickedButton);
        int col = Grid.GetColumn(clickedButton);

        if (board[row, col] != null) return;
        board[row, col] = currentPlayer;
        clickedButton.Text = currentPlayer;
        clickedButton.TextColor = currentPlayer == "X" ? XColor : OColor; // Устанавливаем цвет текста сразу

        if (CheckWin(currentPlayer))
        {
            EndGame($"{currentPlayer} võitis!");
            return;
        }

        if (IsBoardFull())
        {
            EndGame("Viik!");
            return;
        }

        currentPlayer = "O";
        BotMove();
    }

    private void BotMove()
    {
        if (gameOver) return;

        //  Проверить, может ли бот выиграть
        if (TryMakeWinningMove("O")) return;

        //  Проверить, может ли игрок выиграть в следующий ход и заблокировать его
        if (TryMakeWinningMove("X")) return;

        //  Выбрать лучший доступный ход (центр, углы, рандом)
        MakeSmartMove();
    }

    private bool TryMakeWinningMove(string player)
    {
        for (int i = 0; i < 3; i++)
        {
            // Проверка строк
            if (CheckAndPlace(player, i, 0, i, 1, i, 2)) return true;
            // Проверка столбцов
            if (CheckAndPlace(player, 0, i, 1, i, 2, i)) return true;
        }

        // Проверка диагоналей
        if (CheckAndPlace(player, 0, 0, 1, 1, 2, 2)) return true;
        if (CheckAndPlace(player, 0, 2, 1, 1, 2, 0)) return true;

        return false;
    }

    private bool CheckAndPlace(string player, int r1, int c1, int r2, int c2, int r3, int c3)
    {
        string[] values = { board[r1, c1], board[r2, c2], board[r3, c3] };

        if (values.Count(v => v == player) == 2 && values.Count(v => string.IsNullOrEmpty(v)) == 1)
        {
            if (string.IsNullOrEmpty(board[r1, c1])) { PlaceMove(r1, c1); return true; }
            if (string.IsNullOrEmpty(board[r2, c2])) { PlaceMove(r2, c2); return true; }
            if (string.IsNullOrEmpty(board[r3, c3])) { PlaceMove(r3, c3); return true; }
        }

        return false;
    }

    private void MakeSmartMove()
    {
        //  Центр
        if (string.IsNullOrEmpty(board[1, 1])) { PlaceMove(1, 1); return; }

        //  Углы
        int[,] corners = { { 0, 0 }, { 0, 2 }, { 2, 0 }, { 2, 2 } };
        for (int i = 0; i < corners.GetLength(0); i++)
        {
            int row = corners[i, 0];
            int col = corners[i, 1];
            if (string.IsNullOrEmpty(board[row, col]))
            {
                PlaceMove(row, col);
                return;
            }
        }

        //  Оставшиеся клетки (рандом)
        List<Button> emptyCells = new List<Button>();
        foreach (var child in GameBoard.Children)
        {
            if (child is Button button && string.IsNullOrEmpty(button.Text))
            {
                emptyCells.Add(button);
            }
        }

        if (emptyCells.Count > 0)
        {
            Button botChoice = emptyCells[randChoose.Next(emptyCells.Count)];
            PlaceMove(Grid.GetRow(botChoice), Grid.GetColumn(botChoice));
        }
    }

    private void PlaceMove(int row, int col)
    {
        foreach (var child in GameBoard.Children)
        {
            if (child is Button button && Grid.GetRow(button) == row && Grid.GetColumn(button) == col)
            {
                if (string.IsNullOrEmpty(board[row, col]))
                {
                    board[row, col] = "O";
                }
                button.Text = "O";
                button.TextColor = OColor; 

                if (CheckWin("O"))
                {
                    EndGame("O võitis!");
                    return;
                }

                if (IsBoardFull())
                {
                    EndGame("Viik!");
                    return;
                }

                currentPlayer = "X";
                return;
            }
        }
    }

    private bool CheckWin(string player)
    {
        for (int i = 0; i < 3; i++)
        {
            // Проверка строк
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                return true;
            // Проверка столбцов
            if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                return true;
        }

        // Проверка диагоналей
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
            return true;
        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            return true;

        return false;
    }

    private bool IsBoardFull()
    {
        foreach (var cell in board)
        {
            if (string.IsNullOrEmpty(cell)) return false;
        }
        return true;
    }

    private void EndGame(string message)
    {
        gameOver = true;
        WinnerLabel.Text = message;
        RestartGrid.IsVisible = true;
    }

    private void OnRestartClicked(object sender, EventArgs e)
    {
        gameOver = false;
        currentPlayer = "X";
        board = new string[3, 3];

        foreach (var child in GameBoard.Children)
        {
            if (child is Button button)
            {
                button.Text = "";
                button.TextColor = Colors.Black; 
            }
        }

        WinnerLabel.Text = "";
        RestartGrid.IsVisible = false;
    }

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

    private void OnResetColorsClicked(object sender, EventArgs e)
    {
        XColor = Colors.Red;
        OColor = Colors.Blue;

        XColorEntry.Text = string.Empty;
        OColorEntry.Text = string.Empty;

        UpdateBoardColors();
    }

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
                await DisplayAlert("Värv Copid", $"{selectedColor} on lõikelauale kopeeritud!", "OK");
            }
            else
            {
                await DisplayAlert("Viga", "Arhiveeritud kopeeritud värviline lõikelaud.", "OK");
            }
        }
    }
}
