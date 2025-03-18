namespace secondMobileApp
{
    public partial class App : Application
    {
        private static EuroopaRiigid.RiigidDatabase database;
        public static EuroopaRiigid.RiigidDatabase Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "riigid.db3");
                    database = new EuroopaRiigid.RiigidDatabase(dbPath);
                }
                return database;
            }
        }

        public App()
        {



            InitializeComponent();

            MainPage = new NavigationPage(new AppShell());
        }
    }
}
