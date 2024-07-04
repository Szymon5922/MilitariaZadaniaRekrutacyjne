using ProductsBrowser.MappingStrategies;
using ProductsBrowser.Models;

namespace ProductsBrowser.Services
{
    public class ProductMapper
    {
        private readonly IProductMappingStrategy _strategy;
        public ProductMapper(IProductMappingStrategy strategy)
        {
             _strategy = strategy;
        }
        public ProductModel MapProduct(object supplierProduct) 
        {
            return _strategy.MapProduct(supplierProduct);
        }
    }
}
