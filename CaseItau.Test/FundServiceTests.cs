using CaseItau.Data;
using CaseItau.Data.Entities;
using CaseItau.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CaseItau.Test
{

    public class FundServiceTests
    {
        private readonly Mock<FundDbContext> _mockContext;
        private readonly Mock<DbSet<Fund>> _mockDbSet;
        private readonly Mock<ILogger<FundService>> _mockLogger;
        private readonly FundService _fundService;

        public FundServiceTests()
        {
            var services = new ServiceCollection();            
            _mockContext = new Mock<FundDbContext>();
            _mockDbSet = new Mock<DbSet<Fund>>();
            _mockLogger = new Mock<ILogger<FundService>>();

            _mockContext.Setup(m => m.Set<Fund>()).Returns(_mockDbSet.Object);
            _mockDbSet.Setup(d => d.SingleOrDefaultAsync(It.IsAny<Expression<Func<Fund, bool>>>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync((Expression<Func<Fund, bool>> predicate) =>
              {
                  return new Fund() { Code = "ITAUTESTE01" };
              });
            _fundService = new FundService(_mockContext.Object, _mockLogger.Object);            
        }

        [Fact]
        public async Task GetAsync_ReturnsFund()
        {
            // Arrange
            var fund = new Fund { Code = "ITAUTESTE01" };            

            // Act
            var result = await _fundService.GetAsync(x => x.Code == "ITAUTESTE01");

            // Assert
            Assert.Equal("ITAUTESTE01", result.Code);
        }
     

        [Fact]
        public async Task SingleOrDefaultAsync_ReturnsFund()
        {
            // Arrange
            var fund = new Fund { Code = "ITAUTESTE01" };            

            // Act
            var result = await _fundService.SingleOrDefaultAsync(x => x.Code == "ITAUTESTE01");

            // Assert
            Assert.Equal("ITAUTESTE01", result.Code);            
        }

        [Fact]
        public async Task AddAsync_AddsFund()
        {
            // Arrange            
            var fund = new Fund { Code = "ITAUTESTE01" };

            // Act
            await _fundService.AddAsync(fund);

            // Assert
            _mockDbSet.Verify(m => m.AddAsync(fund, default), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public void Update_UpdatesFund()
        {
            // Arrange
            var fund = new Fund { Code = "ITAUTESTE01" };

            // Act
            _fundService.Update(fund);

            // Assert
            _mockDbSet.Verify(m => m.Update(fund), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Remove_RemovesFund()
        {
            // Arrange
            var fund = new Fund { Code = "ITAUTESTE01" };

            // Act
            _fundService.Remove(fund);

            // Assert
            _mockDbSet.Verify(m => m.Remove(fund), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task ExecuteQueryAsync_ReturnsFilteredFunds()
        {
            // Arrange
            var funds = new List<Fund>
            {
                new Fund { Code = "ITAUTESTE01" },
                new Fund { Code = "ITAUTESTE02" }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<Fund>>().Setup(m => m.Provider).Returns(funds.Provider);
            _mockDbSet.As<IQueryable<Fund>>().Setup(m => m.Expression).Returns(funds.Expression);
            _mockDbSet.As<IQueryable<Fund>>().Setup(m => m.ElementType).Returns(funds.ElementType);
            _mockDbSet.As<IQueryable<Fund>>().Setup(m => m.GetEnumerator()).Returns(funds.GetEnumerator());

            // Act
            var result = await _fundService.ExecuteQueryAsync(q => q.Where(f => f.Code == "ITAUTESTE01").AsQueryable());

            // Assert            
            Assert.Equal("ITAUTESTE01", result.First().Code);            
        }
    }

}