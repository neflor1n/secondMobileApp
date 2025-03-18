namespace secondMobileApp.EuroopaRiigid
{
    public partial class AddRiikPage : ContentPage
    {
        public AddRiikPage()
        {
            InitializeComponent();
        }

        private async void SaveRiik_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EntryNimetus.Text) &&
                !string.IsNullOrWhiteSpace(EntryPealinn.Text) &&
                !string.IsNullOrWhiteSpace(EntryRahvastik.Text) &&
                !string.IsNullOrWhiteSpace(EntryLipp.Text))
            {
                var newRiik = new Riik
                {
                    Nimetus = EntryNimetus.Text,
                    Pealinn = EntryPealinn.Text,
                    RahvastikuSuurus = int.Parse(EntryRahvastik.Text),
                    Lipp = EntryLipp.Text
                };

                await App.Database.AddRiikAsync(newRiik);
                await Navigation.PopAsync(); 
            }
            else
            {
                await DisplayAlert("Viga", "Palun täitke kõik väljad!", "ОК");
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
