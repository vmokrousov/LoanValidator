using LoanValidator.Entities;
using LoanValidator.Entities.Enums;
using LoanValidator.Service.Common;
using LoanValidator.Service.Dto;
using LoanValidator.Service.Enums;
using System;
using System.Linq;

namespace LoanValidator.Service.Handling
{
    public class VirginiaVerifier : IVerifier
    {
        const int MaximumPercentThatCanBeCharged = 7;
        private string[] feesIncludedInTotal = { Constants.Fee.FloodCertification, Constants.Fee.Processing, Constants.Fee.Settlement };

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

            // Run test only if Loan type is Conventional or FHA or VA
            if (loan.LoanType == LoanType.Conventional || loan.LoanType == LoanType.FHA || loan.LoanType == LoanType.VA)
            {
                #region # Check Arp
                if (loan.Address.IsPrimaryOccupancy && loan.LoanType == LoanType.VA)
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
                    if (loan.ARP > 8)
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

                // Check loan Fee
                
                if (loan.FeesIncludedInTotal.Intersect(feesIncludedInTotal).Count() == 3)
                {
                    if (loan.Fee > MaximumPercentThatCanBeCharged)
                    {
                        // Error
                        feeTestObject.Status |= LoanVerificationResultCode.TotalFeesExceedTheAmount;
                        feeTestObject.Errors.Add(new VerificationErrorDto(ErrorCode.LoanTotalFeesInvalid,
                            Constants.ErrorPayloadType.Loan.LoanTotalFeesInvalidMsg));
                    }

                    // Add test results for Fee
                    result.Tests.Add(feeTestObject);
                }
            }

            return result;
        }
    }
}
