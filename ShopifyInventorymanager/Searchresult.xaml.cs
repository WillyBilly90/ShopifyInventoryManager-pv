using ShopifyInventorymanager.Helpers;

namespace ShopifyInventorymanager;

public partial class Searchresult : ContentPage
{
	public List<ProductVariant> Products { get; set; }
	public Searchresult(List<ProductVariant> productVariants)
	{
		InitializeComponent();
		Products= productVariants;
		searchResults.BindingContext= this;
	}

    private void searchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		ProductVariant selectedVariant= (ProductVariant)e.SelectedItem;
		Navigation.PushAsync(new DetailPage(selectedVariant));
    }
}