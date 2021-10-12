﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebAPI.Models;

namespace SalesWebAPI.Models
{
    public class AppDbContext : DbContext
    {

        //DbSet collections go here
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) { } // empty until fluent API is needed

        public DbSet<SalesWebAPI.Models.Orderline> Orderline { get; set; } // error occurs on plural. find and fix all plurals or dle
    }
}
