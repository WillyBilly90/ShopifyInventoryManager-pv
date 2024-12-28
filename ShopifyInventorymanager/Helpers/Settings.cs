using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyInventorymanager.Helpers
{
    internal static class Settings
    {
        public static string BaseUrl
        {
            get { return Preferences.Get(nameof(BaseUrl), ""); }
            set { Preferences.Set(nameof(BaseUrl), value); }
        }
        public static string ApiKey
        {
            get { return Preferences.Get(nameof(ApiKey), ""); }
            set { Preferences.Set(nameof(ApiKey),value);}
        }
    }
}
