using ArtShop.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtShop.Data
{
    public class DutchContext : IdentityDbContext<StoreUser>
    {
       

        public DutchContext(DbContextOptions<DutchContext> dbContextOptions) : base(dbContextOptions)
        {
           
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
   
    }
}
