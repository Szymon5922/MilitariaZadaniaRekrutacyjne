using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Xml.Serialization;

namespace ProductsBrowser.Models.Supplier1
{
    [XmlRoot("offer")]
    public class Offer:IOfferBase
    {
        [XmlArray("products")]
        [XmlArrayItem("product")]
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

    public class Product:IProductBase
    {
        int IProductBase.Id => Id;
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlElement("producer")]
        public Producer Producer { get; set; }
        [XmlElement("category")]
        public Category Category { get; set; }
        [XmlElement("warranty")]
        public Warranty Warranty { get; set; }
        [XmlElement("description")]
        public Description Description { get; set; }

        [XmlElement("price")]
        public Price Price { get; set; }

        [XmlElement("srp")]
        public Price Srp { get; set; }
        [XmlElement("images")]
        public Images Images { get; set; }

        [XmlArray("sizes")]
        [XmlArrayItem("size")]
        public List<Size> Sizes { get; set; }
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
    public class Producer
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
    public class Category
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
    public class Warranty
    {
        [XmlAttribute("period")]
        public int Period { get; set; }
    }
    public class Description
    {
        [XmlElement("name")]
        public List<Name> Names { get; set; }
        [XmlElement("version")]
        public VersionDetails Version { get; set; }
        [XmlElement("long_desc")]
        public List<DescriptionDetails> LongDescriptions { get; set; }
        [XmlElement("short_desc")]
        public List<DescriptionDetails> ShortDescriptions { get; set; }
        public class VersionDetails
        {
            [XmlAttribute("name")]
            public string Name { get; set; }
            [XmlElement("name")]
            public List<Name> Names { get; set; }
        }
        public class Name
        {
            [XmlAttribute("xml:lang")]
            public string Language { get; set; }
            [XmlText]
            public string Value { get; set; }
        }
        public class DescriptionDetails
        {
            [XmlAttribute("xml:lang")]
            public string Language { get; set; }
            [XmlText]
            public string Value { get; set; }
        }

    }
    public class Images
    {
        [XmlArray("large")]
        [XmlArrayItem("image")]
        public List<Image> Large { get; set; }
        public class Image
        {
            [XmlAttribute("url")]
            public string Url { get; set; }
        }
    }
    public class Price
    {
        [XmlAttribute("gross")]
        public decimal Gross { get; set; }

        [XmlAttribute("net")]
        public decimal Net { get; set; }

        [XmlAttribute("vat")]
        public decimal Vat { get; set; }
    }

    public class Size
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("weight")]
        public int Weight { get; set; }

        [XmlElement("stock")]
        public Stock Stock { get; set; }
    }

    public class Stock
    {

        [XmlAttribute("quantity")]
        public int Quantity { get; set; }
    }
}
