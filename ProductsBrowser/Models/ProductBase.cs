using System.Xml.Serialization;

namespace ProductsBrowser.Models
{
    public interface IOfferBase
    {
        IEnumerable<IProductBase> Products { get; }
        void Merge(IOfferBase other);
    }
    public interface IProductBase
    {
        [XmlIgnore]
        int Id { get; }
        void Merge(IProductBase other);
    }
}
