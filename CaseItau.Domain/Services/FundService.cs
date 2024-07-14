using CaseItau.Data.Entities;
using CaseItau.Domain.Interfaces;
using CaseItau.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseItau.Domain.Services
{
    public class FundService : GenericRepository<Fund>, IFundService
    {
        public FundService(DbContext context) : base(context)
        {
        }
    }
}
