using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Apis.Orders.Db.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, Models.Order>();
            CreateMap<OrderItem, Models.OrderItem>();
        }
    }
}
