using Microsoft.AspNetCore.Mvc;
using ProductsBrowser.MappingStrategies;
using ProductsBrowser.Models;
using ProductsBrowser.Services;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ProductsBrowser.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ProductsBrowserService _service;
        private List<string> _xmlSupplier1 =>  new List<string> { "data/dostawca1plik1.xml","data/dostawca1plik2.xml"};
        private List<string> _xmlSupplier2 =>  new List<string> { "data/dostawca2plik1.xml","data/dostawca2plik2.xml"};
        private string _xmlSupplier3 =>   "data/dostawca3plik1.xml";
        public Products Products;

        public HomeController(IWebHostEnvironment webHostEnvironment, ProductsBrowserService service)
        {
            _webHostEnvironment = webHostEnvironment;
            _service = service;
            Products = new();
            Products.ProductsList = new();

            var OfferSupplier1 = service.DeserializeOffer<Models.Supplier1.Offer>(_xmlSupplier1);
            var OfferSupplier2 = service.DeserializeOffer<Models.Supplier2.Offer>(_xmlSupplier2);
            var OfferSupplier3 = service.DeserializeOffer<Models.Supplier3.Offer>(_xmlSupplier3);

            Products.ProductsList.AddRange(service.MapOfferToProducts(OfferSupplier1, new Supplier1ProductMappingStrategy()));
            Products.ProductsList.AddRange(service.MapOfferToProducts(OfferSupplier2, new Supplier2ProductMappingStrategy()));
            Products.ProductsList.AddRange(service.MapOfferToProducts(OfferSupplier3, new Supplier3ProductMappingStrategy()));
        }

        public IActionResult Index()
        {
            return View(Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
