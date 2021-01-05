using Ecommerce.Api.Search.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;

        public SearchService(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }
        public async Task<(bool isSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await ordersService.GetOrdersAsync(customerId);
            if(orderResult.isSuccess)
            {
                var result = new
                {
                    Orders = orderResult.Orders
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}
