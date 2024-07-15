using CaseItau.Data.Entities;
using CaseItau.Domain.DTO;
using CaseItau.Domain.Interfaces;
using CaseItau.Repositories;
using CaseItau.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain.Services
{
    public class FundService : GenericRepository<Fund>, IFundService
    {
        private readonly ILogger<FundService> _logger;
        public FundService(DbContext context, ILogger<FundService> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Fund>> GetPaginationFundsAsync(int pageNumber, int pageSize)
        {            
            try
            {
                var funds = await ExecuteQueryAsync((x) => x
                       .AsNoTracking()
                       .Include(f => f.Type)
                       .Skip((pageNumber - 1) * pageSize)
                       .Take(pageSize)
                   );

                return funds.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
