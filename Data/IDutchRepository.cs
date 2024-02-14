using ArtShop.Data.Entities;
using System.Collections.Generic;

namespace ArtShop.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Order> GetAllOrders(bool itemsInclude);
        IEnumerable<Product> GetAllProductsByCategory(string category);
        bool SaveAll();
        Order GetOrderById(int id);
        void AddEntity(object model);
    }
}