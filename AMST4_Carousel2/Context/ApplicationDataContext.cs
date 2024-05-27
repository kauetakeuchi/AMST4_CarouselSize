﻿using AMST4_Carousel2.Models;
using Microsoft.EntityFrameworkCore;

namespace AMST4_Carousel2.Context
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) 
        { 
        
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Size> Size { get; set; }
    }
}
