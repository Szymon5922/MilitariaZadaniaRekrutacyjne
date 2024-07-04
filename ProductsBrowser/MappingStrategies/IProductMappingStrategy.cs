using ProductsBrowser.Models;

namespace ProductsBrowser.MappingStrategies
{
    public interface IProductMappingStrategy
    {
        ProductModel MapProduct(object supplierProduct);
    }
}
