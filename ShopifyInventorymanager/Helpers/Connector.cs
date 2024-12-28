using Newtonsoft.Json;
using ShopifyInventorymanager.GraphQLClient;
using ShopifyInventorymanager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace ShopifyInventorymanager
{
    public static class Connector
    {
        public static List<ProductVariant> GetProductsByBarcode(string productBarcode)
        {
            var GraphQl = new GraphQLNETClient();
            string url = Helpers.Settings.BaseUrl + "/admin/api/2024-01/graphql.json";
            string productId = "";

            string qu = "{products(first: 5, query: \"barcode:" + productBarcode + "\") " +
                "{edges " +
                    "{node " +
                        "{id variants(first: 100) " +
                            "{edges " +
                                "{node " +
                                    "{id" +
                                    " sku" +
                                    " barcode" +
                                    " price" +
                                    " inventoryQuantity" +
                                    " displayName" +
                                    " title" +
                                    " inventoryItem" +
                                    "{id" +
                                    " inventoryLevels(first:5)" +
                                        "{edges" +
                                            "{node" +
                                                "{location" +
                                                    "{id" +
                                                    " name" +
                                                    "}" +
                                                " id" +
                                                " available" +
                                                "}" +
                                    " }}}}}}}}}}";
            var jSonResponse = GraphQl.Execute(url,Helpers.Settings.ApiKey,qu);
            Rootobject objectje = JsonConvert.DeserializeObject<Rootobject>((jSonResponse.ToString()));
            List<ProductVariant> ProductVariants = new List<ProductVariant>();
            foreach (Edge edges in objectje.data.products.edges)
            {
                productId = edges.node.id;
                foreach (Edge1 edge1 in edges.node.variants.edges)
                {
                    foreach (Edge2 edge2 in edge1.node.inventoryItem.inventoryLevels.edges)
                    {
                        ProductVariant productVariant = new ProductVariant();
                        productVariant.variantId = edge1.node.id;
                        productVariant.inventoryLevelId = edge2.node.id;
                        productVariant.sku = edge1.node.sku;
                        productVariant.price = edge1.node.price;
                        productVariant.title = edge1.node.title;
                        productVariant.barcode = edge1.node.barcode;
                        productVariant.displayName = edge1.node.displayName;
                        productVariant.inventoryAvailable = edge2.node.available;
                        productVariant.inventoryLocationName = edge2.node.location.name;
                        ProductVariants.Add(productVariant);
                    }
                }
            };

            return ProductVariants;

        }
        public static List<ProductVariant> GetProductsByDisplayName(string Searchterm)
        {
            var GraphQl = new GraphQLNETClient();
            string url = Helpers.Settings.BaseUrl + "/admin/api/2023-10/graphql.json";
            string productId = "";


            string qu = "{products(first: 50, query: \"title:" + Searchterm + "*" + "\") " +
                "{edges " +
                    "{node " +
                        "{title" +
                        " id variants(first: 100) " +
                        "{edges " +
                            "{node " +
                                "{id" +
                                " sku" +
                                " barcode" +
                                " price" +
                                " inventoryQuantity" +
                                " title" +
                                " displayName" +
                                " inventoryItem" +
                                    "{id" +
                                    " inventoryLevels(first:5)" +
                                        "{edges" +
                                            "{node" +
                                                "{location" +
                                                    "{id" +
                                                    " name" +
                                                    "}" +
                                                " id" +
                                                " available" +
                                                "}" +
                "}}}}}}}}}}";
            var jSonResponse = GraphQl.Execute(url,Helpers.Settings.ApiKey, qu);
            Rootobject objectje = JsonConvert.DeserializeObject<Rootobject>((jSonResponse.ToString()));
            List<ProductVariant> ProductVariants = new List<ProductVariant>();
            foreach (Edge edges in objectje.data.products.edges)
            {
                productId = edges.node.id;
                foreach (Edge1 edge1 in edges.node.variants.edges)
                {
                    foreach (Edge2 edge2 in edge1.node.inventoryItem.inventoryLevels.edges)
                    {
                        ProductVariant productVariant = new ProductVariant();
                        productVariant.variantId = edge1.node.id;
                        productVariant.inventoryLevelId = edge2.node.id;
                        productVariant.sku = edge1.node.sku;
                        productVariant.price = edge1.node.price;
                        productVariant.title = edge1.node.title;
                        productVariant.productId = productId;
                        productVariant.barcode = edge1.node.barcode;
                        productVariant.displayName = edge1.node.displayName;
                        productVariant.inventoryAvailable = edge2.node.available;
                        productVariant.inventoryLocationName = edge2.node.location.name;
                        productVariant.displayName=productVariant.displayName.Replace(" - "+productVariant.title, "");
                        ProductVariants.Add(productVariant);
                    }
                }
            };

            return ProductVariants;
        }

        public static Rootobject UpdateInventory(string inventoryLevelId, int inventoryChange)
        {
            var GraphQl = new GraphQLNETClient();
            string url = Helpers.Settings.BaseUrl + "/admin/api/2024-01/graphql.json";
            string input = "{inventoryLevelId:\"" + inventoryLevelId + "\", availableDelta:" + inventoryChange + "}";
            string qu = "mutation " +
                "{inventoryAdjustQuantity(input: " + input + ") " +
                    "{inventoryLevel " +
                        "{id" +
                        " quantities(names: [\"available\"]) " +
                            "{name" +
                            " quantity" +
                            "}" +
                        " incoming" +
                        " item" +
                            "{id" +
                            " sku" +
                            "}" +
                        " location" +
                            "{id" +
                            " name" +
                        "}" +
                    "}" +
                "}}";


            var jSonResponse = GraphQl.Execute(url,Helpers.Settings.ApiKey, qu);
            Rootobject objectje = JsonConvert.DeserializeObject<Rootobject>((jSonResponse.ToString()));


            return objectje;
        }
        public static Rootobject UpdatePrice(string ProductVariantId, String Price)
        {
            var GraphQl = new GraphQLNETClient();
            string url = Helpers.Settings.BaseUrl + "/admin/api/2022-01/graphql.json";
            string qu = "mutation " +
                "{productVariantUpdate(input: {id: \"" + ProductVariantId + "\",price:"+Price+"}) {" +
                    " productVariant {" +
                       "id" +
                       " title" +
                       " inventoryPolicy" +
                       " inventoryQuantity" +
                       " price" + 
               "}" +
               "userErrors {" +
                       "field" +
                       " message" +
                    "}" +
                  "}" +
                "}";

            //string qu= "mutation {productUpdate(input: {id: \"gid://shopify/Product/8148348436784\",    variants:{\r\n      price:300.00\r\n    }\r\n  }) {\r\n    product {\r\n      id\r\n    }\r\n  }\r\n} "

            var jSonResponse = GraphQl.Execute(url, Helpers.Settings.ApiKey, qu);
            Rootobject objectje = JsonConvert.DeserializeObject<Rootobject>((jSonResponse.ToString()));


            return objectje;
        }
    }
}