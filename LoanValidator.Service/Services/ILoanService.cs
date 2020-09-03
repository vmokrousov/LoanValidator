using LoanValidator.Service.Dto;


namespace LoanValidator.Service.Services
{
    public interface ILoanService
    {
        VerificationResultDto Verify(int loanId);
    }
}
