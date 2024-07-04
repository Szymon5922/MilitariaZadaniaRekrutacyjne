using ProductsBrowser.Models;

namespace ProductsBrowser.MappingStrategies
{
    public class Supplier1ProductMappingStrategy : IProductMappingStrategy
    {
        public ProductModel MapProduct(object supplierProduct)
        {
            var product = (Models.Supplier1.Product)supplierProduct;
            return new ProductModel
            {
                Id = product.Id,
                Quantity = null, //because product have no overall stock quantity, each size have specified stock quantity
                EAN = null, //no ean provided in dostawca1 files
                Name = product.Description!=null? product.Description.Names.FirstOrDefault(n => n.Language == "pol").Value:null,
                Categories = product.Category!=null?product.Category.Name.Split('/').ToList():null,
                Description = product.Description != null ? product.Description.LongDescriptions.FirstOrDefault(d => d.Language == "pol").Value : null,
                WarrantyPeriod = product.Warranty!=null?product.Warranty.Period:null,
                Versions = new List<Models.Version>
                {
                    new Models.Version
                    {
                        Name = product.Description != null ?product.Description.Version.Names.FirstOrDefault(v => v.Language == "pol").Value:null,
                        Quantity = product.Sizes.FirstOrDefault().Stock.Quantity
                    }
                },
                Price = new Price
                {
                    Gross = product.Price.Gross,
                    Net = product.Price.Net
                },
                Srp = new Price
                {
                    Gross = product.Srp.Gross,
                    Net = product.Price.Net
                },
                Vat = (int)product.Price.Vat,
                PhotosUrls = product.Images!=null?product.Images.Large.Select(i=>i.Url).ToList():null
            };
        }
    }
}
