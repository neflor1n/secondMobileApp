
namespace secondMobileApp;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>() { new TextPage(0), new FigurePage(1), new Clicker_Page(2) };
	public List<String> tekstid = new List<string> { "Tee lahti TextPage", "Tee lahti FigurePage", "Clicker" };

	ScrollView sv;
	VerticalStackLayout vst;
	public StartPage()
	{
		Title = "Avaleht";

        vst = new VerticalStackLayout { BackgroundColor = Color.FromRgb(180, 100, 20) };
		for (int i = 0; i < tekstid.Count; i++) { 
				Button nupp = new Button()
				{
					Text = tekstid[i],
					BackgroundColor = Color.FromArgb("#9b2f91"),
                    TextColor = Color.FromArgb("#38ebcb"),
					FontFamily = "Minecraft",
					FontSize = 32,
					BorderWidth = 10,
					ZIndex = i
				};
			vst.Add(nupp);
            nupp.Clicked += Nupp_Clicked;
        }
		sv = new ScrollView { Content = vst };
		Content = sv;
    }

    private async void Nupp_Clicked(object? sender, EventArgs e)
    {
        Button btn = sender as Button;	
		await Navigation.PushAsync(lehed[btn.ZIndex]);	
    }
}