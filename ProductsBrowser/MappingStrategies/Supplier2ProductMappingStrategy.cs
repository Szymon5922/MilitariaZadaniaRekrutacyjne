using ProductsBrowser.Models;

namespace ProductsBrowser.MappingStrategies
{
    public class Supplier2ProductMappingStrategy:IProductMappingStrategy
    {
        public ProductModel MapProduct(object supplierProduct)
        {
            var product = (Models.Supplier2.Product)supplierProduct;
            return new ProductModel
            {
                Id = product.Id,
                Quantity = product.Quantity,
                EAN = product.Ean,
                Name = product.Name,
                Categories = product.Categories,
                WarrantyPeriod = null,
                Versions = null,
                Price = new Price
                {
                    Gross = Math.Round((Convert.ToDecimal(product.PriceNet.Replace(',', '.'))) * ((decimal)product.Vat + 100 )/ 100,2),
                    Net = Convert.ToDecimal(product.PriceNet.Replace(',', '.'))
                },
                Srp = new Price
                {
                    Gross = product.RetailPriceGross,
                    Net = Math.Round(product.RetailPriceGross * (decimal)(100 - product.Vat) / 100,2)
                },
                Vat = product.Vat,
                PhotosUrls = product.PhotosUrls
            };
        }
    }
}
