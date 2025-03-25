namespace secondMobileApp.Views;

public partial class NotePage : ContentPage
{
    string _filename = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

	public NotePage(int k)
	{
		InitializeComponent();
        if (File.Exists(_filename))
        {
            TextEditor.Text = File.ReadAllText(_filename);
        }
	}

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (File.Exists(_filename))
            File.Delete(_filename);

        TextEditor.Text = string.Empty;
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        File.WriteAllText (_filename, TextEditor.Text);
    }
}