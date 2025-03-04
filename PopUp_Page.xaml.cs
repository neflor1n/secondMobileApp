namespace secondMobileApp;

public partial class PopUp_Page : ContentPage
{
	public PopUp_Page(int k)
	{
		Button alertButton = new Button()
		{
			Text = "Teade",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
        alertButton.Clicked += AlertButton_Clicked;
		Button alertYesNoButton = new Button()
		{
			Text = "Jah või ei",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
        alertYesNoButton.Clicked += AlertYesNoButton_Clicked;
		Button alertListButton = new Button()
		{
			Text = "Valik",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
        alertListButton.Clicked += AlertListButton_Clicked;

        Button alertQuest = new Button()
		{
			Text = "Küsimus",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
        alertQuest.Clicked += AlertQuest_Clicked;
		Content = new StackLayout { Children = { alertButton, alertYesNoButton, alertListButton, alertQuest } };
		
	}

    private async void AlertQuest_Clicked(object? sender, EventArgs e)
    {
		string result1 = await DisplayPromptAsync("Küsimus", "Kuidas läheb?", placeholder: "Tore!");
		string result = await DisplayPromptAsync("Vasta", "Millega võrdub 5 + 5?", initialValue:"10", maxLength: 2, keyboard: Keyboard.Numeric);
    }

    private async void AlertListButton_Clicked(object? sender, EventArgs e)
    {
		var action = await DisplayActionSheet("Mida teha?", "Loobu", "Kustutada", "Tantsida", "Laulda", "Joonistada");
    }

    private async void AlertYesNoButton_Clicked(object? sender, EventArgs e)
    {
		bool result = await DisplayAlert("Kinnitus", "Kas oled kindel?", "Olen Kindel", "Ei ole kindel");
		await DisplayAlert("Teade", "Teie valik on " + (result ? "Jah" : "Ei"), "OK");
    }

    private void AlertButton_Clicked(object? sender, EventArgs e)
    {
		DisplayAlert("Teade", "Teil on uus teade", "OK");
    }
}