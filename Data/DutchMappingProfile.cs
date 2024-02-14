using AutoMapper;
using ArtShop.Data.Entities;
using ArtShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtShop.Data
{
    public class DutchMappingProfile:Profile
    {

        public DutchMappingProfile()
        {
            CreateMap<Order, OrdersViewModel>().ForMember(o=>o.OrderId, ex=>ex.MapFrom(o=>o.Id)).ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
        }
    }
}
