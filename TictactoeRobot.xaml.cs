namespace secondMobileApp;

public partial class TictactoeRobot : ContentPage
{
    private string currentPlayer = "X";
    private string[,] board = new string[3, 3]; 
    private bool gameOver = false;
    private Random randChoose = new Random();

    public TictactoeRobot()
    {
        InitializeComponent();
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
            }
        }

        WinnerLabel.Text = "";
        RestartGrid.IsVisible = false;
    }
}
