using ProductsBrowser.Models;

namespace ProductsBrowser.Services
{
    public interface IProductsBrowserService
    {
        T DeserializeOffer<T>(List<string> xmlPaths) where T : IOfferBase;
    }
}
