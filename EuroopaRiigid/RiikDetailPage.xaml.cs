namespace secondMobileApp.EuroopaRiigid;

public partial class RiikDetailPage : ContentPage
{
    public RiikDetailPage(Riik riik)
    {
        InitializeComponent();
        BindingContext = riik;
    }
}