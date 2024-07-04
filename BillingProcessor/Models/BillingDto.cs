using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcessor.Models
{
    public class BillingDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public TypeDetails Type { get; set; }
        [JsonProperty("offer")]
        public OfferDetails Offer { get; set; }
        [JsonProperty("value")]
        public ValueDetails Value { get; set; }
        [JsonProperty("tax")]
        public TaxDetails Tax { get; set; }
        [JsonProperty("order")]
        public OrderDetails Order { get; set; }
        public struct TypeDetails
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
        }
        public struct OfferDetails
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
        }
        public struct ValueDetails
        {
            [JsonProperty("amount")]
            public string Amount { get; set; }
            [JsonProperty("currency")]
            public string Currency { get; set; }
        }
        public struct TaxDetails
        {
            [JsonProperty("percentage")]
            public string Percentage { get; set; }
            [JsonProperty("annotation")]
            public string Annotation { get; set; }
        }
        public struct OrderDetails
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }

    }
}
