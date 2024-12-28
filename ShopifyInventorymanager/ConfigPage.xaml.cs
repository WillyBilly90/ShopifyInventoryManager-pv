namespace ShopifyInventorymanager;

public partial class ConfigPage : ContentPage
{
	public ConfigPage()
	{
		InitializeComponent();
		BaseurlEntry.Text = Helpers.Settings.BaseUrl;
		ApikeyEntry.Text = Helpers.Settings.ApiKey;
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
		OkBtn.IsEnabled = false;
		if (Uri.IsWellFormedUriString(BaseurlEntry.Text, UriKind.Absolute))
			if (ApikeyEntry.Text.Length == 38)
				OkBtn.IsEnabled = true;
    }

    private async void OkBtn_Clicked(object sender, EventArgs e)
    {
		Helpers.Settings.BaseUrl=BaseurlEntry.Text;
		Helpers.Settings.ApiKey=ApikeyEntry.Text;
        await DisplayAlert("Bijgewerkt","Instellingen opgeslagen", "OK");
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}