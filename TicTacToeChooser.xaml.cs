namespace secondMobileApp;

public partial class TicTacToeChooser : ContentPage
{
	public TicTacToeChooser(int k)
	{
		InitializeComponent();
	}

    private async void Bot_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TictactoeRobot());
    }

    private async void Player_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TickTackToe());
    }
}