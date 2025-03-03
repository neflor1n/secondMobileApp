using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace secondMobileApp;

public partial class RGBSlider : ContentPage
{
    private Slider redSlider, greenSlider, blueSlider;
    private BoxView colorDisplay;
    private Label HexLabel;
    private Button SaveBtn;
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

        SaveBtn = new Button
        {
            WidthRequest = 200,
            HeightRequest = 40,
            CornerRadius = 15,
            Text = "Salvesta HexCode",
            FontFamily = "Minecraft",
            FontSize = 14

        };

        SaveBtn.Clicked += async (s, e) =>
        {
            await Clipboard.SetTextAsync(HexLabel.Text);
            await DisplayAlert("Salvestamine", "Salvestatud Clipboard'is", "OK");
        };


        var inputHex = new Entry
        {
            Placeholder = "Your Hexcode here...",
            FontFamily = "Minecraft",
            TextColor = Colors.Black,
            FontSize = 16

        };

        inputHex.TextChanged += (s, e) =>
        {
            string hex = e.NewTextValue;
            if (IsValidHex(hex))
            {
                colorDisplay.Color = Color.FromArgb(hex);
                HexLabel.Text = hex;
                
                int red = Convert.ToInt32(hex.Substring(1, 2), 16);
                int green = Convert.ToInt32(hex.Substring(3, 2), 16);
                int blue = Convert.ToInt32(hex.Substring(5, 2), 16);

                redSlider.Value = red; 
                greenSlider.Value = green;
                blueSlider.Value = blue;

                
                
            }
        };



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

        var Frame = new HorizontalStackLayout
        {
            Spacing = 10,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(10, 10, 10, 10),
            Children = { HexLabel, SaveBtn }
        };

        var colorDisplayLayout = new VerticalStackLayout
        {
            Children = { Frame, colorDisplay, inputHex  }
        };

        
        var mainLayout = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10,
            Children = { layout, colorDisplayLayout}
        };



       



        redSlider.ValueChanged += (sender, e) => UpdateColor();
        greenSlider.ValueChanged += (sender, e) => UpdateColor();
        blueSlider.ValueChanged += (sender, e) => UpdateColor();

        Content = new ScrollView
        {
            Content = mainLayout
        };
    }

    private bool IsValidHex(string hex)
    {
        if (string.IsNullOrEmpty(hex) || hex.Length != 7 || hex[0] != '#')
            return false;

        return System.Text.RegularExpressions.Regex.IsMatch(hex.Substring(1), @"^[0-9A-Fa-f]{6}$");
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
