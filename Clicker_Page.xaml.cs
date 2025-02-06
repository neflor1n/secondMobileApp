namespace secondMobileApp;

public partial class Clicker_Page : ContentPage
{
    Button Clickerbtn;
    Button Upgradebtn;
    int score = 0;
    Label scoreLabel;
    Label upgradeLabel;
    int upgradeCost = 20;
    bool upgradeAvailable = false;
    int lvl = 0;
    int upgradeLvl;

    public Clicker_Page(int k)
    {
        InitializeComponent();

        Title = "Clicker";

        scoreLabel = new Label
        {
            Text = "Score: 0",
            FontSize = 24,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            TextColor = Colors.White
        };

        upgradeLabel = new Label
        {
            Text = $"Upgrade: {upgradeCost} score",
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            TextColor = Colors.White,
            IsVisible = false
        };

        Clickerbtn = new Button
        {
            ImageSource = "clicker_icon.png",
            BackgroundColor = Color.FromArgb("#00000000"),
            BorderColor = Color.FromArgb("#00000000"),
            WidthRequest = 350,
            HeightRequest = 350,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };

        Upgradebtn = new Button
        {
            Text = $"Upgrade. LVL: {lvl}",
            WidthRequest = 200,
            HeightRequest = 50,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.End,
            IsVisible = false
        };

        Clickerbtn.Clicked += (sender, e) =>
        {
            score++;
            scoreLabel.Text = $"Score: {score}";
            UpdateButtonIcon();

            if (score >= upgradeCost && !upgradeAvailable)
            {
                upgradeLabel.IsVisible = true;
                Upgradebtn.IsVisible = true;
                upgradeAvailable = true;
            }
        };

        Upgradebtn.Clicked += (sender, e) =>
        {
            if (score >= upgradeCost)
            {
                score -= upgradeCost;
                lvl++;
                Upgradebtn.Text = $"Upgrade. LVL: {lvl}";
                scoreLabel.Text = $"Score: {score}";
                upgradeCost = (int)(upgradeCost * 2.5);
                upgradeLabel.Text = $"Upgrade: {upgradeCost} score";
                Clickerbtn.Clicked -= DefaultClick;
                Clickerbtn.Clicked += DoubleClick;
            }
        };

        Content = new Grid
        {
            BackgroundColor = Colors.Black, 
            Children = {
                new VerticalStackLayout
                {
                    Children = { scoreLabel, upgradeLabel },
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start
                },
                Clickerbtn,
                Upgradebtn
            }
        };
    }

    private void DefaultClick(object sender, EventArgs e)
    {
        score++;
        scoreLabel.Text = $"Score: {score}";
        UpdateButtonIcon(); 
    }

    private void DoubleClick(object sender, EventArgs e)
    {
        score += 2;
        scoreLabel.Text = $"Score: {score}";
        UpdateButtonIcon(); 
    }

    private void UpdateButtonIcon()
    {
        if (score >= 1000)
        {
            Clickerbtn.ImageSource = "clicker_icon5.png";
        }
        else if (score >= 500)
        {
            Clickerbtn.ImageSource = "clicker_icon4.png";
        }
        else if (score >= 100)
        {
            Clickerbtn.ImageSource = "clicker_icon3.png";
        }
        else if (score >= 10)
        {
            Clickerbtn.ImageSource = "clicker_icon2.png";
        }
        else
        {
            Clickerbtn.ImageSource = "clicker_icon.png";
        }
    }
}
