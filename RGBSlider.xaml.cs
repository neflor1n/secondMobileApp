using Microsoft.Maui.Controls;

namespace secondMobileApp;

public partial class RGBSlider : ContentPage
{
    private Slider redSlider, greenSlider, blueSlider;
    private BoxView colorDisplay;

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

        var layout = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10,
            Children =
            {
                new Label { Text = "Red" },
                redSlider,
                new Label { Text = "Green" },
                greenSlider,
                new Label { Text = "Blue" },
                blueSlider
            }
        };

        var colorDisplayLayout = new VerticalStackLayout
        {
            Children = { colorDisplay }
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
    }
}
