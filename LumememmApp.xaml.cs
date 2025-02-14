using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace secondMobileApp
{
    public class Lumememm : ContentPage
    {
        private Frame head, body, baseCircle, bucket;
        private Label actionLabel;
        private Random random = new Random();

        public Lumememm(int k)
        {
            // ведро
            bucket = new Frame
            {
                WidthRequest = 60,
                HeightRequest = 40,
                BackgroundColor = Colors.Gray,
                BorderColor = Colors.Black
            };

            //  голова снеговика
            head = new Frame
            {
                WidthRequest = 80,
                HeightRequest = 80,
                CornerRadius = 40,
                BackgroundColor = Colors.White,
                BorderColor = Colors.Black
            };

            // тело снеговика
            body = new Frame
            {
                WidthRequest = 110,
                HeightRequest = 110,
                CornerRadius = 55,
                BackgroundColor = Colors.White,
                BorderColor = Colors.Black
            };

            // нижняя часть снеговика
            baseCircle = new Frame
            {
                WidthRequest = 140,
                HeightRequest = 140,
                CornerRadius = 70,
                BackgroundColor = Colors.White,
                BorderColor = Colors.Black
            };

            AbsoluteLayout snowmanLayout = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutBounds(bucket, new Rect(110, 20, 60, 40));
            AbsoluteLayout.SetLayoutBounds(head, new Rect(100, 60, 80, 80));
            AbsoluteLayout.SetLayoutBounds(body, new Rect(85, 140, 110, 110));
            AbsoluteLayout.SetLayoutBounds(baseCircle, new Rect(70, 250, 140, 140));

            snowmanLayout.Children.Add(bucket);
            snowmanLayout.Children.Add(head);
            snowmanLayout.Children.Add(body);
            snowmanLayout.Children.Add(baseCircle);

            actionLabel = new Label
            {
                Text = "Tegevus: ---",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 20,
                FontFamily = "Minecraft"
            };

            

            Button hideButton = new Button { Text = "Peida" };
            hideButton.Clicked += (s, e) => ToggleVisibility(false);

            Button showButton = new Button { Text = "Näita" };
            showButton.Clicked += (s, e) => ToggleVisibility(true);

            Button colorButton = new Button { Text = "Muuda värvi" };
            colorButton.Clicked += (s, e) => ChangeColor();

            Button meltButton = new Button { Text = "Sulata" };
            meltButton.Clicked += async (s, e) => await Melt();

            Stepper sizeStepper = new Stepper(0.5, 2, 1, 0.1);
            sizeStepper.ValueChanged += (s, e) => Resize(e.NewValue);


            var frameForBtns = new HorizontalStackLayout
            {
                Spacing = 10,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 10, 10, 10),
                Children = { hideButton, showButton, colorButton, meltButton }
            };

            StackLayout controlBtns = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(0, 70, 0, 0),
                Children = { frameForBtns }
            };

            StackLayout controlSizes = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(0, 50, 0,0 ),
                Children = {sizeStepper}

            };

            Content = new VerticalStackLayout
            {
                Padding = 20,
                Children = { actionLabel, snowmanLayout, controlBtns, controlSizes }
            };
        }

        private void ToggleVisibility(bool isVisible)
        {
            bucket.IsVisible = isVisible;
            head.IsVisible = isVisible;
            body.IsVisible = isVisible;
            baseCircle.IsVisible = isVisible;
            actionLabel.Text = isVisible ? "Tegevus: Näitan lumememme" : "Tegevus: Peidan lumememme";
        }

        private void ChangeColor()
        {
            Color randomColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
            head.BackgroundColor = randomColor;
            body.BackgroundColor = randomColor;
            baseCircle.BackgroundColor = randomColor;
            actionLabel.Text = "Tegevus: Muutsin värvi";
        }

        private async Task Melt()
        {
            actionLabel.Text = "Tegevus: Lumememm sulab...";
            for (int i = 10; i >= 0; i--)
            {
                double scale = i / 10.0;
                head.Scale = scale;
                body.Scale = scale;
                baseCircle.Scale = scale;
                bucket.Scale = scale;
                await Task.Delay(200);
            }
            actionLabel.Text = "Tegevus: Lumememm on sulanud!";
        }

        private void Resize(double scale)
        {
            head.Scale = scale;
            body.Scale = scale;
            baseCircle.Scale = scale;
            bucket.Scale = scale;
            actionLabel.Text = $"Tegevus: Suuruse muutmine {scale:0.0}x";
        }
    }
}
