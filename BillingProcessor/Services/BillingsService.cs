using AutoMapper;
using BillingProcessor.ApiHandling;
using BillingProcessor.Entities;
using BillingProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcessor.Services
{
    public class BillingsService
    {
        private ApplicationDbContext _dbContext;
        public BillingsService(string connectionString)
        {
            _dbContext = new ApplicationDbContext(connectionString);
        }
        public List<string> GetOrdersIds()
        {
            var ordersIds = from o in _dbContext.OrderTable
                            select
                            o.orderId;

            return ordersIds.ToList();
        }
        public async Task<List<BillingDto>> GetBillingEntries(List<string> orderIds)
        {
            List<BillingDto> billingsDtos = new();

            foreach(string orderId in orderIds)
                billingsDtos.Add(await BillingsProcessor.LoadBilling(orderId));


            return billingsDtos;
        }
        public void SaveBillingInformations(List<BillingDto> billings)
        {
            var mappferConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BillingMappingProfile>();
            });

            IMapper mapper = mappferConfig.CreateMapper();

            foreach (var billingDto in billings)
            {
                Billing billing = mapper.Map<Billing>(billingDto);
                _dbContext.BillingsTable.Add(billing);
            }

            _dbContext.SaveChanges();
        }
    }
}
