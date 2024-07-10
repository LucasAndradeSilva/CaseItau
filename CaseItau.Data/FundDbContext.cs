using CaseItau.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseItau.Data
{
    public class FundDbContext : DbContext
    {
        public FundDbContext(DbContextOptions<FundDbContext> options) : base(options)
        {
        }

        public DbSet<Fund> Funds { get; set; }
    }
}
