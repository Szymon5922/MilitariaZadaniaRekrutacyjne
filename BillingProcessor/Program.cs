using BillingProcessor.Entities;
using BillingProcessor.ApiHandling;
using BillingProcessor.Models;
using BillingProcessor;
using Serilog;
using BillingProcessor.Services;

string connectionString = "YOUR_CONNECTION_STRING";
string bearerToken = "YOUR_BEARER_TOKEN";

BillingsService service = new BillingsService(connectionString); 
ApiHelper.InitializeHttpClient(bearerToken);
Log.Logger = Logger.CreateLogger();


List<string> orderIds = new();
try
{
    orderIds = service.GetOrdersIds();
}
catch(Exception ex)
{
    Log.Information(ex.Message);
}
if(orderIds.Count() == 0)
{
    Log.Information("OrderTable is empty");
    return;
}

List<BillingDto> billings = new();
try
{
    billings = await service.GetBillingEntries(orderIds);
}
catch (Exception ex)
{
    Log.Information(ex.Message);
    return;
}

try
{
    service.SaveBillingInformations(billings);
}
catch (Exception ex)
{
    Log.Information(ex.Message);
}