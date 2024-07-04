using System;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

class Program
{
    static void Main(string[] args)
    {
        // Konfiguracja serwera WireMock
        var server = WireMockServer.Start(new WireMockServerSettings
        {
            Urls = new[] { "http://localhost:8080" },
            ReadStaticMappings = true,
            WatchStaticMappings = true,
        });

        server.Given(Request.Create()
            .WithPath("/some/api")
            .UsingGet()
            .WithHeader("Authorization", "*", MatchBehaviour.RejectOnMatch))
            .RespondWith(Response.Create()
                .WithStatusCode(401));

        server
            .Given(Request.Create()
            .WithPath("/some/api")
            .UsingGet()
            .WithHeader("Authorization", new WildcardMatcher("Bearer *")))
            .RespondWith(Response.Create()
                .WithHeader("Content-Type", "application/json")
                .WithBodyAsJson((request)=>
                {
                    var paramValue = request.Query["order.id"].FirstOrDefault();

                    ResponseModel response = new(paramValue);
                    
                    return response;
                }
                ));



        Console.WriteLine("WireMock server running at http://localhost:8080/");
        Console.WriteLine("Press any key to stop the server...");
        
        Console.ReadKey();

        // Zatrzymanie serwera WireMock
        server.Stop();
    }
}
public class ResponseModel
{
    public string id { get; set; }
    public DateTime occuredAt => GetRandomDateTime(new DateTime(2020, 1, 1), DateTime.Now);
    public TypeDetails type => GetRandomTypeDetails();
    public OfferDetails offer => GetRandomOfferDetails();
    public ValueDetails value { get; set; }
    public TaxDetails tax => GetRandomTaxDetails();
    public BalanceDetails balance { get; set; }
    public OrderDetails order { get; set; }
    public struct TypeDetails
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public struct OfferDetails
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public struct ValueDetails
    {
        public string amount => GenerateRandomMoneyValue(10, 10000).ToString();
        public string currency => "PLN";
    }
    public struct TaxDetails
    {
        public string percentage { get; set; }
        public string annotation { get; set; }
    }
    public struct BalanceDetails
    {
        public string amount => GenerateRandomMoneyValue(-10000, 10000).ToString();
        public string currency => "PLN";
    }
    public struct OrderDetails
    {
        public string id { get; set; }
    }

    public ResponseModel(string orderId)
    {
        id = orderId;
        value = new ValueDetails();
        balance = new BalanceDetails();
        order = new OrderDetails
        {
            id = orderId
        };
    }
    private DateTime GetRandomDateTime(DateTime startDate, DateTime endDate)
    {
        Random random = new Random();

        int range = (endDate - startDate).Days;

        int randomDays = random.Next(range);

        DateTime randomDate = startDate.AddDays(randomDays);

        randomDate = randomDate.AddHours(random.Next(24))
                               .AddMinutes(random.Next(60))
                               .AddSeconds(random.Next(60))
                               .AddMilliseconds(random.Next(1000));

        return randomDate;
    }
    private TypeDetails GetRandomTypeDetails()
    {
        string[][] typeValues =
        {
            new string[]{"SUC","Prowizja od sprzedaży"},
            new string[]{"LIS","Wystawienie przedmiotu"}
        };

        int randomNumber = GenerateRandomNumber(typeValues.Length);

        return new TypeDetails
        {
            id = typeValues[randomNumber][0],
            name = typeValues[randomNumber][1]
        };
    }
    private OfferDetails GetRandomOfferDetails()
    {
        string offerId = GenerateRandomNumber(1000000, 9999999).ToString();
        return new OfferDetails
        {
            id = offerId,
            name = "oferta "  + offerId.Substring(0, 3)

        };
    }
    private TaxDetails GetRandomTaxDetails()
    {
        int[] taxRates = { 0, 23 };
        string[] annotations = { "EXEMPT", "NOT_APPLICABLE" };

        int taxPercentage = taxRates[GenerateRandomNumber(taxRates.Length)];
        string taxAnnotation = null;

        if(taxPercentage == 0)
            taxAnnotation = annotations[GenerateRandomNumber(annotations.Length)];

        return new TaxDetails
        {
            percentage = taxPercentage.ToString(),
            annotation = taxAnnotation
        };
    }
    private static int GenerateRandomNumber(int maxValue)
    {
        Random rng = new Random();
        return rng.Next(maxValue);
    }
    private static int GenerateRandomNumber(int minValue,int maxValue)
    {
        Random rng = new Random();
        return rng.Next(minValue, maxValue);
    }
    static decimal GenerateRandomMoneyValue(decimal minValue, decimal maxValue)
    {
        Random random = new Random();

        double randomDouble = random.NextDouble();

        decimal randomValue = (decimal)(randomDouble * (double)(maxValue - minValue) + (double)minValue);

        decimal roundedValue = Math.Round(randomValue, 2);

        return roundedValue;
    }
}