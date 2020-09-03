using FluentAssertions;
using LoanValidator.Api.Controllers;
using LoanValidator.Service.Dto;
using LoanValidator.Service.Services;
using Moq;
using Xunit;

namespace LoanValidator.Api.Tests
{
    public class LoanControllerTests : ControllerTestBase<LoanController>
    {
        private readonly Mock<ILoanService> _loanServiceMock;


        public LoanControllerTests()
        {
            _loanServiceMock = new Mock<ILoanService>();

            Controller = new LoanController(_loanServiceMock.Object);
        }

        [Fact]
        public void ControllerShouldBeAvailable()
        {
            Controller.Should().NotBeNull();
        }

        [Fact]
        public void Test_ShouldVerify_Success()
        {
            // Assets
            VerificationResultDto verificationResult = new VerificationResultDto();
            int loanId = 123456;

            _loanServiceMock
                .Setup(x => x.Verify(loanId))
                .Returns(verificationResult);
            // Action
            var result = Controller.Verify(loanId);

            // Assert
            _loanServiceMock.Verify(x => x.Verify(loanId), Times.Once);
            result.Should().NotBeNull();
        }
    }
}