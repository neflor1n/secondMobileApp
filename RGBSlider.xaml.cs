using Microsoft.Maui.Controls;

namespace secondMobileApp;

public partial class RGBSlider : ContentPage
{
    private Slider redSlider, greenSlider, blueSlider;
    private BoxView colorDisplay;
    private Label HexLabel;

    public RGBSlider(int k)
    {
        redSlider = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0
        };

        greenSlider = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0
        };

        blueSlider = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0
        };

        colorDisplay = new BoxView
        {
            HeightRequest = 400,
            WidthRequest = 400
        };

        HexLabel = new Label
        {
            Text = "#000000",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontFamily = "Minecraft",
            FontSize = 24
        };

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += (s, e) =>
        {
            Clipboard.SetTextAsync(HexLabel.Text); // это что бы скопировать hexCode в буфер обмена
            DisplayAlert("Copied", $"HEX code {HexLabel.Text} copied to clipboard", "OK");
        };

        HexLabel.GestureRecognizers.Add(tapGestureRecognizer);

        var layout = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10,
            Children =
            {
                new Label { FontFamily = "Minecraft", Text = "Red" },
                redSlider,
                new Label { FontFamily = "Minecraft", Text = "Green" },
                greenSlider,
                new Label { FontFamily = "Minecraft", Text = "Blue" },
                blueSlider
            }
        };

        var colorDisplayLayout = new VerticalStackLayout
        {
            Children = { HexLabel, colorDisplay  }
        };

        var mainLayout = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10,
            Children = { layout, colorDisplayLayout }
        };
        
         
        

        redSlider.ValueChanged += (sender, e) => UpdateColor();
        greenSlider.ValueChanged += (sender, e) => UpdateColor();
        blueSlider.ValueChanged += (sender, e) => UpdateColor();

        Content = mainLayout;
    }

    private void UpdateColor()
    {
        var red = (int)redSlider.Value;
        var green = (int)greenSlider.Value;
        var blue = (int)blueSlider.Value;

        colorDisplay.Color = Color.FromRgb(red, green, blue);

        string hexCode = $"#{red:X2}{green:X2}{blue:X2}";
        HexLabel.Text = hexCode;

    }
}
