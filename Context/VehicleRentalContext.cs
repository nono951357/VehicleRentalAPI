﻿using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Context
{
    public class VehicleRentalContext : DbContext
    {
        public VehicleRentalContext(DbContextOptions<VehicleRentalContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}
