namespace secondMobileApp;

public partial class StartPage : ContentPage
{
    public List<ContentPage> lehed = new List<ContentPage>() { new TextPage(0), new FigurePage(1), new Clicker_Page(2), new secondMobileApp.Valgusfoor(3) };
    public List<String> tekstid = new List<string> { "Tee lahti TextPage", "Tee lahti FigurePage", "Clicker", "Valgusfoor" };

    ScrollView sv;
    VerticalStackLayout vst;

    public StartPage()
    {
        Title = "Avaleht";

        
        var grid = new Grid();

        
        var backgroundImage = new Image
        {
            Source = "background.jpg", 
            Aspect = Aspect.Fill 
        };
        grid.Children.Add(backgroundImage);

        
        vst = new VerticalStackLayout
        {
            BackgroundColor = Color.FromArgb("#18000000")
        };

        for (int i = 0; i < tekstid.Count; i++)
        {
            Button nupp = new Button()
            {
                Text = tekstid[i],
                BackgroundColor = Color.FromArgb("#03405C"),
                TextColor = Color.FromArgb("#38ebcb"),
                FontFamily = "Minecraft",
                FontSize = 32,
                BorderWidth = 10,
                ZIndex = i
            };
            vst.Add(nupp);
            nupp.Clicked += Nupp_Clicked;
        }

        
        grid.Children.Add(vst);

        
        sv = new ScrollView { Content = grid };
        Content = sv;
    }

    private async void Nupp_Clicked(object? sender, EventArgs e)
    {
        Button btn = sender as Button;
        await Navigation.PushAsync(lehed[btn.ZIndex]);
    }
}
