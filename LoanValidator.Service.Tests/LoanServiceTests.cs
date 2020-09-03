using FluentAssertions;
using LoanValidator.Service.Dto;
using LoanValidator.Service.Services;
using Moq;
using System.Linq;
using Xunit;

namespace LoanValidator.Service.Tests
{
    public class LoanServiceTests
    {
       
        private readonly LoanService _service;

        public LoanServiceTests()
        {
            _service = new LoanService();
        }

        [Fact]
        public void Verify_ShouldReturnError()
        {
            // Assets New York
            const int loanId = 123456;

            // Action
            var action = _service.Verify(loanId);
            Assert.Single(action.Tests);
        }
    }
}
