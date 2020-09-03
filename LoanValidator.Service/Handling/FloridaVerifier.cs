using LoanValidator.Entities;
using LoanValidator.Entities.Enums;
using LoanValidator.Service.Common;
using LoanValidator.Service.Dto;
using LoanValidator.Service.Enums;
using System;
using System.Linq;

namespace LoanValidator.Service.Handling
{
    public class FloridaVerifier: IVerifier
    {
        const double MaximumLoanAmount = 400.000;
        private string[] feesIncludedInTotal = { Constants.Fee.Application, Constants.Fee.FloodCertification, Constants.Fee.TitleSearch };
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

            // Run test only if MaximumLoanAmount 400.000 and Loan type is Conventional or FHA or VA
            if (loan.LoanAmount >= MaximumLoanAmount &&
                (loan.LoanType == LoanType.Conventional || loan.LoanType == LoanType.FHA || loan.LoanType == LoanType.VA))
            {
                // check loan ARP (All Loans: 4%)
                if (loan.ARP > 4)
                {
                    // Error
                    arpTestObject.Status |= LoanVerificationResultCode.ArpIsExceedRates;
                    arpTestObject.Errors.Add(new VerificationErrorDto(ErrorCode.LoanArpRateIsInvalid,
                        Constants.ErrorPayloadType.Loan.LoanArpRateIsInvalidMsg));
                }

                result.Tests.Add(arpTestObject);

                #region #Check fee
                // Loan amount <= 20,000: 6% 
                // if > 20,000  and <= 75,000: 8%; 
                // if > 75,000 and <= 150,000: 9 %; 
                // if > 150,000: 10 %

                if (loan.FeesIncludedInTotal.Intersect(feesIncludedInTotal).Count() == 2)
                {
                    bool isPassed = true;
                    if (loan.LoanAmount <= 20.000  && loan.Fee > 6)
                    {
                        isPassed = false;
                    }
                    if (loan.LoanAmount > 20.000 && loan.LoanAmount <= 75.000 && loan.Fee > 8)
                    {
                        isPassed = false;
                    }
                    if (loan.LoanAmount > 75.000 && loan.LoanAmount <= 150.000 && loan.Fee > 9)
                    {
                        isPassed = false;
                    }
                    if (loan.LoanAmount > 150.000 && loan.Fee > 10)
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

                result.Tests.Add(feeTestObject);

                #endregion //check fee
            }

            return result;
        }
    }
}
