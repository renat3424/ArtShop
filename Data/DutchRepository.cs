using ArtShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtShop.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;

        public DutchRepository(DutchContext ctx)
        {
            _ctx = ctx;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool itemsInclude)
        {
            if (itemsInclude)
            {
                return _ctx.Orders.Include(o => o.Items).ThenInclude(i => i.Product).ToList();
            }
            else
            {
                return _ctx.Orders.ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {

            return _ctx.Products.OrderBy(p => p.Title).ToList();
        }
        public IEnumerable<Product> GetAllProductsByCategory(string category)
        {

            return _ctx.Products.Where(p => p.Category == category).ToList();
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders.Include(o => o.Items).ThenInclude(i => i.Product).Where(o=>o.Id==id).FirstOrDefault();
        }

        public bool SaveAll()
        {

            return _ctx.SaveChanges()>0;
        }

    }
}
