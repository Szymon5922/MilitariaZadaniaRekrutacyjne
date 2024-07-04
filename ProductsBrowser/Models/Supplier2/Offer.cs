using System.Xml.Serialization;

namespace ProductsBrowser.Models.Supplier2
{
    [XmlRoot("products")]
    public class Offer:IOfferBase
    {
        [XmlElement("product")]
        public List<Product> Products { get; set; }
        IEnumerable<IProductBase> IOfferBase.Products => Products;
        public void Merge(IOfferBase other)
         {
            Dictionary<int, Product> groupedProducts = new();

            foreach (var product in this.Products)
                groupedProducts.Add(product.Id, product);

            foreach (var product in other.Products)
            {
                if (groupedProducts.ContainsKey(product.Id))
                    groupedProducts[product.Id].Merge(product);
                else
                    groupedProducts.Add(product.Id, (Product)product);
            }
        }
    }
    public class Product : IProductBase
    {
        int IProductBase.Id => Id;
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("ean")]
        public string Ean { get; set; }
        [XmlElement("desc")]
        public string Description { get; set; }
        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public List<string> Categories { get; set; }
        [XmlElement("weight")]
        public string Weight { get; set; }
        [XmlElement("qty")]
        public int Quantity { get; set; }
        [XmlElement("priceAfterDiscountNet")]
        public string PriceNet { get; set; }
        [XmlElement("retailPriceGross")]
        public decimal RetailPriceGross { get; set;}
        [XmlElement("vat")]
        public int Vat { get; set; }
        [XmlArray("photos")]
        [XmlArrayItem("photo")]
        public List<string> PhotosUrls { get; set; }

        public void Merge(IProductBase other)
        {
            if (other == null)
                return;

            var properties = typeof(Product).GetProperties();

            foreach (var property in properties)
            {
                var otherValue = property.GetValue(other);

                if (otherValue != null || otherValue is string str && string.IsNullOrWhiteSpace(str))
                    property.SetValue(this, otherValue);
            }
        }
    }
}
