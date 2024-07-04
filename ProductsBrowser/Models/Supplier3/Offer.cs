using System.Xml.Serialization;

namespace ProductsBrowser.Models.Supplier3
{
    [XmlRoot("produkty")]
    public class Offer:IOfferBase
    {
        [XmlElement("produkt")]
        public List<Product> Products { get; set; }

        IEnumerable<IProductBase> IOfferBase.Products => Products;

        public void Merge(IOfferBase other)
        {
            throw new NotImplementedException();
        }
    }
    public class Product:IProductBase
    {
        int IProductBase.Id => Id;
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("nazwa")]
        public string Name { get; set; }
        [XmlElement("cat")]
        public string Category { get; set; }
        [XmlElement("rozmiar")]
        public string Size { get; set; }
        [XmlElement("kod_dostawcy")]
        public string Supplier { get; set; }
        [XmlElement("cena_zewnetrzna_hurt")]
        public decimal Price { get; set; }
        [XmlElement("cena_sugerowana")]
        public decimal Srp { get; set; }
        [XmlElement("vat")]
        public decimal VAT { get; set; }

        [XmlElement("dlugi_opis")]
        public string Description { get; set; }
        [XmlArray("zdjecia")]
        [XmlArrayItem("zdjecie")]
        public List<Photo> Photos { get; set; }
        [XmlElement("ean")]
        public string EAN { get; set; }

        public void Merge(IProductBase other)
        {
            throw new NotImplementedException();
        }
    }
    public class Photo
    {
        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}
