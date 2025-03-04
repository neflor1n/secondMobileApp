namespace secondMobileApp.PopUp_kasutamisvoimalused
{
    public partial class userChooser : ContentPage
    {
        private Button puzzles, dictionary, multiplicationTable;
        private Button reg;
        public List<ContentPage> lehed = new List<ContentPage>()
        {
            new mangud.puzzles(1),
            new mangud.MultiplicationQuizApp(2)
        };
        public List<String> tekstid = new List<string>
        {
            "Puzzles",
            "Multiplication"
        };
        VerticalStackLayout vst;

        public userChooser(int k)
        {
            vst = new VerticalStackLayout();  
            for (int i = 0; i < tekstid.Count; i++)
            {
                Button nupp = new Button()
                {
                    Text = tekstid[i],
                    Margin = new Thickness(0, 5, 0, 5),
                    BackgroundColor = Color.FromArgb("#03405C"),
                    TextColor = Color.FromArgb("#38ebcb"),
                    FontFamily = "Minecraft",
                    FontSize = 14,
                    BorderWidth = 10,
                    ZIndex = i,
                    IsVisible = false  
                };
                vst.Add(nupp);
                nupp.Clicked += Nupp_Clicked;
            }

            reg = new Button
            {
                Text = "Logi sisse",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            reg.Clicked += async (s, e) =>
            {
                string name = await DisplayPromptAsync("Nimi sisestamine", "Palun, sisestage oma nimi:", placeholder: "Nimi");

                if (!string.IsNullOrEmpty(name))
                {
                    UserInfo.UserName = name;

                    foreach (Button nupp in vst.Children)  
                    {
                        nupp.IsVisible = true;  
                    }
                }
                else
                {
                    await DisplayAlert("Viga", "Palun, sisestage oma nimi", "OK");
                }
            };

            Content = new StackLayout
            {
                Children = {
                    reg,
                    vst  
                }
            };
        }

        private async void Nupp_Clicked(object? sender, EventArgs e)
        {
            Button btn = sender as Button;
            await Navigation.PushAsync(lehed[btn.ZIndex]);
        }
    }
}
