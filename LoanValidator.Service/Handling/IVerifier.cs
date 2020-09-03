using LoanValidator.Entities;
using LoanValidator.Service.Dto;

namespace LoanValidator.Service.Handling
{
    public interface IVerifier
    {
        VerificationResultDto Verify(Loan loan);
    }
}
