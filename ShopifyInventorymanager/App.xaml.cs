namespace ShopifyInventorymanager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if ((Helpers.Settings.BaseUrl == "") || (Helpers.Settings.ApiKey == "")) Application.Current.MainPage = new NavigationPage(new ConfigPage());
            else MainPage = new Microsoft.Maui.Controls.NavigationPage(new MainPage());
        }
    }
}
