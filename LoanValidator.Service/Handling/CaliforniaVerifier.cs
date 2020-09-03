using LoanValidator.Entities;
using LoanValidator.Entities.Enums;
using LoanValidator.Service.Common;
using LoanValidator.Service.Dto;
using LoanValidator.Service.Enums;
using System.Linq;

namespace LoanValidator.Service.Handling
{
    public class CaliforniaVerifier : IVerifier
    {
        const double MaximumLoanAmount = 600.000;
        private string[] feesIncludedInTotal = { Constants.Fee.Application, Constants.Fee.Settlement };
        public VerificationResultDto Verify(Loan loan)
        {
            VerificationTestDto arpTestObject =
                new VerificationTestDto
                {
                    Name = $"Test ARP Amount for {loan.Address.State} located loan #{loan.Id}",
                    Status = LoanVerificationResultCode.Success
                };

            VerificationTestDto feeTestObject =
                new VerificationTestDto
                {
                    Name = $"Test Fee Amount for {loan.Address.State} located loan #{loan.Id}",
                    Status = LoanVerificationResultCode.Success
                };

            VerificationResultDto result = new VerificationResultDto();

            // Run test only if MaximumLoanAmount 600.000 and Loan type is Conventional or FHA or VA
            if (loan.LoanAmount >= MaximumLoanAmount &&
                (loan.LoanType == LoanType.Conventional || loan.LoanType == LoanType.FHA || loan.LoanType == LoanType.VA))
            {
                #region # Check Arp
                if (loan.Address.IsPrimaryOccupancy && (loan.LoanType == LoanType.Conventional || loan.LoanType == LoanType.FHA))
                {
                    // check loan ARP
                    if (loan.ARP > 5)
                    {
                        // ARP Amount Error
                        arpTestObject.Status |= LoanVerificationResultCode.ArpIsExceedRates;
                        arpTestObject.Errors.Add(new VerificationErrorDto(ErrorCode.LoanArpRateIsInvalid,
                            Constants.ErrorPayloadType.Loan.LoanArpRateIsInvalidMsg));
                    }
                }

                if (!loan.Address.IsPrimaryOccupancy && loan.LoanType == LoanType.VA)
                {
                    // check loan ARP
                    if (loan.ARP > 3)
                    {
                        // ARP Amount Error
                        arpTestObject.Status |= LoanVerificationResultCode.ArpIsExceedRates;
                        arpTestObject.Errors.Add(new VerificationErrorDto(ErrorCode.LoanArpRateIsInvalid,
                            Constants.ErrorPayloadType.Loan.LoanArpRateIsInvalidMsg));
                    }
                }

                #endregion //Check Arp

                // Add test results for ARP
                result.Tests.Add(arpTestObject);

                #region #Check fee

                // Loan amount <= 50,000: 3%; if >50,000 and <= 150,000: 4%; if > 150,000: 5 %
                if (loan.FeesIncludedInTotal.Intersect(feesIncludedInTotal).Count() == 2)
                {
                    bool isPassed = true;

                    if (loan.LoanAmount <= 50.000 && loan.Fee > 3)
                    {
                        isPassed = false;
                    }
                    if (loan.LoanAmount <= 150.000 && loan.Fee > 4)
                    {
                        isPassed = false;
                    }
                    if (loan.LoanAmount > 150.000 && loan.Fee > 5)
                    {
                        isPassed = false;
                    }

                    if (!isPassed)
                    {
                        // Error
                        feeTestObject.Status |= LoanVerificationResultCode.TotalFeesExceedTheAmount;
                        feeTestObject.Errors.Add(new VerificationErrorDto(ErrorCode.LoanTotalFeesInvalid,
                            Constants.ErrorPayloadType.Loan.LoanTotalFeesInvalidMsg));
                    }

                }
                #endregion //check fee

                // Add test results for Fee
                result.Tests.Add(feeTestObject);
            }

            return result;
        }
    }
}
