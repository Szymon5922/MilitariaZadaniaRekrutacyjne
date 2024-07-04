namespace ProductsBrowser.Models
{
    public class Products
    {
        public List<ProductModel> ProductsList { get; set; }
    }
    public class ProductModel
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public string? EAN { get; set; }
        public string Name { get; set; }
        public List<string> Categories { get; set; }
        public string Description { get; set; }
        public int? WarrantyPeriod { get; set; }
        public List<Version>? Versions { get; set; }
        public Price Price { get; set; }
        public Price Srp { get; set; }
        public int Vat { get; set; }
        public List<string> PhotosUrls { get; set; }
    }
    public class Version
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
    public class Price
    {
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
    }
}
