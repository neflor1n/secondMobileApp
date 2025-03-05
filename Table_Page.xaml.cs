namespace secondMobileApp;

public partial class Table_Page : ContentPage
{
	TableView tabelview;
	SwitchCell sc;
	ImageCell ic;
	TableSection fotosection;
	public Table_Page(int k)
	{
		sc = new SwitchCell { Text = "Näita veel" };
        sc.OnChanged += Sc_OnChanged;
		ic = new ImageCell
		{
			ImageSource = ImageSource.FromFile("ewkere.jpg"),
			Text = "Foto nimetus",
			Detail = "Foto kirjeldus"
		};
		fotosection = new TableSection();

		tabelview = new TableView
		{
			Intent = TableIntent.Form,
			Root = new TableRoot("Andmete sisestamine")
			{
				new TableSection("Põhiandmed:") {
					new EntryCell
					{
						Label = "Nimi:",
						Placeholder = "Sisesta oma sõbra nimi",
						Keyboard = Keyboard.Default
					}
                },
				new TableSection("Kontaktiandmed:")
				{
					new EntryCell
					{
						Label = "Telefon:",
						Placeholder="Sisesta tel. number",
						Keyboard = Keyboard.Telephone
					},
					new EntryCell
					{
						Label = "Email",
						Placeholder = "Sisesta email",
						Keyboard = Keyboard.Email
					},
					sc
				},
				fotosection
            }
		};
	}

    private void Sc_OnChanged(object? sender, ToggledEventArgs e)
    {
        if (e.Value)
		{
			fotosection.Title = "Foto:";
			fotosection.Add(ic);
			sc.Text = "Peida";

		}
		else
		{
			fotosection.Title = "";
			fotosection.Remove(ic);
            sc.Text = "Näita veel";
        }
	}
}