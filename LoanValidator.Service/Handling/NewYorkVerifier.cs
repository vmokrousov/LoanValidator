
using LoanValidator.Entities;
using LoanValidator.Entities.Enums;
using LoanValidator.Service.Common;
using LoanValidator.Service.Dto;
using LoanValidator.Service.Enums;

namespace LoanValidator.Service.Handling
{
    public class NewYorkVerifier : IVerifier
    {
        const double MaximumLoanAmount = 750.000d;
        public VerificationResultDto Verify(Loan loan)
        {
            VerificationTestDto arpTestObject = 
                new VerificationTestDto { Name = $"Test ARP Amount for {loan.Address.State} located loan #{loan.Id}", 
                    Status = LoanVerificationResultCode.Success };

            VerificationResultDto result = new VerificationResultDto();
            
            // Run test only if MaximumLoanAmount 750,000 and Loan type is Conventional
            if (loan.LoanAmount >= MaximumLoanAmount && loan.LoanType == LoanType.Conventional)
            {
                if (loan.Address.IsPrimaryOccupancy)
                {
                    // check loan ARP
                    if (loan.ARP > 6)
                    {
                        // Error
                        arpTestObject.Status |= LoanVerificationResultCode.ArpIsExceedRates;
                        arpTestObject.Errors.Add(new VerificationErrorDto(ErrorCode.LoanArpRateIsInvalid,
                            Constants.ErrorPayloadType.Loan.LoanArpRateIsInvalidMsg));                       
                    }
                }

                if (!loan.Address.IsPrimaryOccupancy)
                {
                    // check loan ARP
                    if (loan.ARP > 8)
                    {
                        // ARP Amount Error
                        arpTestObject.Status |= LoanVerificationResultCode.ArpIsExceedRates;
                        arpTestObject.Errors.Add(new VerificationErrorDto(ErrorCode.LoanArpRateIsInvalid,
                            Constants.ErrorPayloadType.Loan.LoanArpRateIsInvalidMsg));
                    }
                }
            }

            result.Tests.Add(arpTestObject);

            return result;
        }
    }
}
