using ShopifyInventorymanager.Helpers;
using System.Runtime.InteropServices;
using ZXing.Net.Maui;

namespace ShopifyInventorymanager
{
    public partial class MainPage : ContentPage
    {
        List<ProductVariant> productVariants {  get; set; }

        public MainPage()
        {
            InitializeComponent();
            if(OperatingSystem.IsWindows()) cameraBarcodeReaderView.IsVisible = false;
            cameraBarcodeReaderView.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                Formats = BarcodeFormats.OneDimensional,
                AutoRotate = true,
                Multiple = false
            };
        }

        private async void BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            productVariants = new List<ProductVariant>();
            foreach(var barcode in e.Results)
            {
                productVariants.AddRange(Connector.GetProductsByBarcode(barcode.Value));
            }
            MainThread.BeginInvokeOnMainThread(async () => { await Navigation.PushAsync(new Searchresult(productVariants)); });
        }

        private async void SearchTermBtn_Clicked(object sender, EventArgs e)
        {
            productVariants = new List<ProductVariant>();
            productVariants.AddRange(Connector.GetProductsByDisplayName(SearchEntry.Text));
            await Navigation.PushAsync(new Searchresult(productVariants));
        }

        private async void SearchBarcodeBtn_Clicked(object sender, EventArgs e)
        {
            productVariants = new List<ProductVariant>();
            productVariants.AddRange(Connector.GetProductsByBarcode(SearchEntry.Text));
            await Navigation.PushAsync(new Searchresult(productVariants));
        }

        private void SearchEntry_Completed(object sender, EventArgs e)
        {
            SearchBarcodeBtn_Clicked(null, null);
        }
    }

}
