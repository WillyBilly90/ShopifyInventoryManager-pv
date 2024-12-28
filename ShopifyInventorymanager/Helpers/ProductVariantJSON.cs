using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyInventorymanager.Helpers
{

    public class Rootobject
    {
        public Data data { get; set; }
        public Extensions extensions { get; set; }
    }

    public class Data
    {
        public Products products { get; set; }
    }

    public class Products
    {
        public Edge[] edges { get; set; }
    }

    public class Edge
    {
        public Node node { get; set; }
    }

    public class Node
    {
        public string title { get; set; }
        public string id { get; set; }
        public Variants variants { get; set; }
    }

    public class Variants
    {
        public Edge1[] edges { get; set; }
    }

    public class Edge1
    {
        public Node1 node { get; set; }
    }

    public class Node1
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string barcode { get; set; }
        public string price { get; set; }
        public int inventoryQuantity { get; set; }
        public string title { get; set; }
        public string displayName { get; set; }
        public Inventoryitem inventoryItem { get; set; }
    }

    public class Inventoryitem
    {
        public string id { get; set; }
        public Inventorylevels inventoryLevels { get; set; }
    }

    public class Inventorylevels
    {
        public Edge2[] edges { get; set; }
    }

    public class Edge2
    {
        public Node2 node { get; set; }
    }

    public class Node2
    {
        public Location location { get; set; }
        public string id { get; set; }
        public int available { get; set; }
    }

    public class Location
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Extensions
    {
        public Cost cost { get; set; }
    }

    public class Cost
    {
        public int requestedQueryCost { get; set; }
        public int actualQueryCost { get; set; }
        public Throttlestatus throttleStatus { get; set; }
    }

    public class Throttlestatus
    {
        public float maximumAvailable { get; set; }
        public int currentlyAvailable { get; set; }
        public float restoreRate { get; set; }
    }
}
