using ShopifyInventorymanager.Helpers;

namespace ShopifyInventorymanager;

public partial class DetailPage : ContentPage
{
	private ProductVariant thisProductVariant{ get; set; }
	public ProductVariant ThisProductVariant
    {
        get { return thisProductVariant; }
        set
        {
            thisProductVariant = value;
            OnPropertyChanged();
        }
    }

    public int OrigionalInventoryAvailable;
	public DetailPage(ProductVariant SelectedVariant)
	{
		InitializeComponent();
		this.BindingContext=this;
		ThisProductVariant = SelectedVariant;
        OrigionalInventoryAvailable = SelectedVariant.inventoryAvailable;
	}

    private async void UpdateBtn_Clicked(object sender, EventArgs e)
    {
        int inventoryChange=ThisProductVariant.inventoryAvailable-OrigionalInventoryAvailable;
        Connector.UpdatePrice(ThisProductVariant.variantId, ThisProductVariant.price);
        Connector.UpdateInventory(ThisProductVariant.inventoryLevelId, inventoryChange);
        await DisplayAlert("Bijwerken",ThisProductVariant.displayName+" is bijgewerkt", "OK");
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }

    private void PriceEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        {
            if (e.NewTextValue.Length > 0)
            {
                if(e.NewTextValue.Contains(",")) PriceEntry.Text = e.NewTextValue.Replace(",",".");
                if (!double.TryParse(e.NewTextValue, out _)) PriceEntry.Text = e.OldTextValue;
                if (e.NewTextValue.Contains("."))
                {
                    if (e.NewTextValue.Length - 1 - e.NewTextValue.IndexOf(".") > 2)
                    {
                        var s = e.NewTextValue.Substring(0, e.NewTextValue.IndexOf(".") + 2 + 1);
                        PriceEntry.Text = s;
                        PriceEntry.SelectionLength = s.Length;
                    }
                }
            }
            EnableDisableUpdateButton();
        }
    }

    private void InventoryEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(e.NewTextValue.Length>0) if (!int.TryParse(e.NewTextValue, out _)) InventoryEntry.Text = e.OldTextValue;
        EnableDisableUpdateButton();
    }

    private void EnableDisableUpdateButton()
    {
        UpdateBtn.IsEnabled = false;
        if (PriceEntry.Text != null & InventoryEntry.Text != null)
            if (PriceEntry.Text.Length > 0 & InventoryEntry.Text.Length > 0) UpdateBtn.IsEnabled = true;
    }
}