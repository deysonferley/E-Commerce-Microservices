using AutoMapper;
using Ecommerce.Api.Customers.Db.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Db.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext customersDbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext customersDbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.customersDbContext = customersDbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if(!customersDbContext.Customers.Any())
            {
                customersDbContext.Customers.Add(new Customer() { Id = 1, Name = "James", Address= "Nairobi"});
                customersDbContext.Customers.Add(new Customer() { Id = 2, Name = "Jill", Address = "Thika" });
                customersDbContext.Customers.Add(new Customer() { Id = 3, Name = "Joe", Address = "Roysambu" });
                customersDbContext.Customers.Add(new Customer() { Id = 4, Name = "Jane", Address = "Ruaka"});
                customersDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Querying customers information.");
                var customers = await customersDbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);

                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSucess, Models.Customer customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                logger?.LogInformation("Querying customer information.");
                var customer = await customersDbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (customer != null)
                {
                    var result = mapper.Map<Customer, Models.Customer>(customer);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }

            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

    }
}
