using CaseItau.Data.Entities;
using CaseItau.Repositories;
using CaseItau.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain.Interfaces
{
    public interface IFundService : IGenericRepository<Fund>
    {
        Task<IEnumerable<Fund>> GetPaginationFundsAsync(int pageNumber = 1, int pageSize = 20);
    }
}
