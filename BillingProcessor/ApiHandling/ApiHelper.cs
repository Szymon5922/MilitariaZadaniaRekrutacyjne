using Azure;
using BillingProcessor.Entities;
using BillingProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcessor.ApiHandling
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static void InitializeHttpClient(string bearerToken)
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("https://api.allegro.pl/billing/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", bearerToken);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
    public static class BillingsProcessor
    {
        public static async Task<BillingDto> LoadBilling(string orderId)
        {
            string param = $"billing-entries?order.id={orderId}";
            using (HttpResponseMessage responseMessage = await ApiHelper.ApiClient.GetAsync(param))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    BillingDto billing = await responseMessage.Content.ReadAsAsync<BillingDto>();
                    return billing;
                }
                else
                    throw new Exception(responseMessage.ReasonPhrase);
            };
        }
    }
}
