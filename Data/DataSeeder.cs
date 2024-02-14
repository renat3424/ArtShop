using ArtShop.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArtShop.Data
{
    public class DataSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;

        public DataSeeder(DutchContext ctx, IHostingEnvironment env, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _env = env;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {


            _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("galifanov.renat@gmail.com");
            if (user == null)
            {

                user = new StoreUser()
                {

                    FirstName = "Renat",
                    LastName = "Galifanov",
                    Email = "galifanov.renat@gmail.com",
                    UserName = "galifanov.renat@gmail.com",

                };
                var result = await _userManager.CreateAsync(user, "Ren@t3004");

                if (result != IdentityResult.Success)
                {

                    throw new InvalidOperationException("Не вышло создать пользователя");
                }

            }
            if (!_ctx.Products.Any())
            {

                
                var filePath = Path.Combine(_env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);
                var order = new Order()
                {
                    User = user,
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    Items = new List<OrderItem>() {
                        new OrderItem()
                        {

                            Product = products.First(),
                            Quantity=5,
                            UnitPrice=products.First().Price


                        }
                    
                    
                    }
                    

                };

                _ctx.Orders.Add(order);
                _ctx.SaveChanges();
            }


        }
    }
}
