using System.Collections.ObjectModel;

namespace secondMobileApp.EuroopaRiigid
{
    public partial class Riiigd : ContentPage
    {
        public ObservableCollection<Riik> Riigid { get; set; } = new ObservableCollection<Riik>();

        public Riiigd(int k)
        {
            InitializeComponent();
            RiigidListView.ItemsSource = Riigid; // Привязка списка
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData(); // Обновляю список при возврате
        }

        private async Task LoadData()
        {
            var riigidList = await App.Database.GetRiigidAsync();
            Riigid.Clear();
            foreach (var riik in riigidList)
            {
                Riigid.Add(riik);
            }
        }

        private async void OpenAddRiikPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRiikPage());
        }

        private async void RiigidListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Riik selectedRiik)
            {
                await Navigation.PushAsync(new RiikDetailPage(selectedRiik));
            }
        }

        private async void DeleteRiik_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter is int riikId)
            {
                var riikToDelete = await App.Database.GetRiikByIdAsync(riikId);
                if (riikToDelete != null)
                {
                    await App.Database.DeleteRiikAsync(riikToDelete);
                    await LoadData(); // Обновляем список после удаления
                }
            }
        }
    }
}
