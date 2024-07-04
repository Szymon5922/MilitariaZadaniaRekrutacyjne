using ProductsBrowser.Models;

namespace ProductsBrowser.MappingStrategies
{
    public class Supplier3ProductMappingStrategy:IProductMappingStrategy
    {
        public ProductModel MapProduct(object supplierProduct)
        {
            var product = (Models.Supplier3.Product)supplierProduct;
            return new ProductModel
            {
                Id = product.Id,
                Quantity = null, //supplier does not provide information about quantity
                EAN = product.EAN,
                Name = product.Name,
                Categories = new List<string> { product.Category },
                Description = product.Description,
                WarrantyPeriod = null,
                Versions = new List<Models.Version>
                {
                    new Models.Version
                    {
                        Name = product.Size
                    }
                },
                Price = new Price
                {
                    Gross = Math.Round(product.Price + product.Price * product.VAT,2),
                    Net = product.Price
                },
                Srp = new Price
                {
                    Gross = product.Srp,
                    Net = Math.Round(product.Srp * (1 - product.VAT),2)
                },
                Vat = (int)(product.VAT * 100),
                PhotosUrls = product.Photos.Select(p => p.Url).ToList()
            };
        }
    }
}
