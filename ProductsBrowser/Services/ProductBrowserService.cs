using ProductsBrowser.MappingStrategies;
using ProductsBrowser.Models;
using System.Xml.Serialization;
namespace ProductsBrowser.Services
{
    public class ProductsBrowserService:IProductsBrowserService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsBrowserService(IWebHostEnvironment webHost)
        {
            _webHostEnvironment = webHost;
        }
        public List<ProductModel> MapOfferToProducts (IOfferBase offer, IProductMappingStrategy strategy)
        {
            List<ProductModel> products = new();

            var mapper = new ProductMapper(strategy);

            foreach (var product in offer.Products)
                products.Add(mapper.MapProduct(product));

            return products;
        }
        public T DeserializeOffer<T>(List<string> xmlPaths) where T : IOfferBase
        {
            T offer = DeserializeOffer<T>(xmlPaths[0]);

            for (int i = 1; i < xmlPaths.Count; i++)
                offer.Merge(DeserializeOffer<T>(xmlPaths[i]));

            return offer;
        }
        public T DeserializeOffer<T>(string xmlPath)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, xmlPath);

            T offer;
            var serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(path))
            {
                offer = (T)serializer.Deserialize(reader);
            }
            return offer;
        }
    }
}
