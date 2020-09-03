using LoanValidator.Entities;
using LoanValidator.Entities.Enums;
using LoanValidator.Service.Common;
using LoanValidator.Service.Dto;
using LoanValidator.Service.Enums;
using LoanValidator.Service.Handling;
using System.Collections.Generic;
using System.Linq;

namespace LoanValidator.Service.Services
{
    public class LoanService : ILoanService
    {

        public VerificationResultDto Verify(int loanId)
        {
            
            VerificationResultDto result;
            // Move logic to _loanService

            // Step 1: Load loan data from Database (Moq data)
            Loan loan = MoqLoanData().FirstOrDefault(x => x.Id == loanId);

            if (loan != null)
            {
                // Step 2: Verify loan           
                IVerifier verifier = GetVerifier(loan.Address.State);
                result = verifier.Verify(loan);
            }
            else 
            {
                throw new System.Exception($"Requested Loan with id = {loanId} has been deleted or doesn't exist.");
            }

            return result;
        }

        /// <summary>
        /// Factory method
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private IVerifier GetVerifier(string state)
        {
            
            switch (state)
            {
                case "NY":
                    return new NewYorkVerifier();
                case "VA":
                    return new VirginiaVerifier();
                case "MD":
                    return new MarylandVerifier();
                case "CA":
                    return new CaliforniaVerifier();
                case "FL":
                    return new FloridaVerifier();
                default:
                    throw new System.Exception("There no proper verifier was found for provided state. Please add to factory");
            }
        }

        private List<Loan> MoqLoanData()
        {
            var result = new List<Loan>();

            // Part 1: Add Loan # 1
            Loan loan1 = new Loan
            {
                Id = 123456,
                LoanType = LoanType.VA,
                LoanAmount = 800.000,
                ARP = 9,
                Fee = 8888,
                FeesIncludedInTotal = new string[] { "Flood Certification", "Processing", "Settlement" }
            };

            // Part 2: Address for Loan # 1
            loan1.Address = new Address
            {
                Id = 1,
                LoanId = 123456,
                IsPrimaryOccupancy = true,
                State = "VA"
            };

            // Part 3: Add Loan # 2
            Loan loan2 = new Loan
            {
                Id = 1234567,
                LoanType = LoanType.VA,
                ARP = 2,
                Fee = 8,
                FeesIncludedInTotal = new string [] { "Flood Certification", "Processing", "Settlement" }
            };

            // Part 4: Address for Loan # 2
            loan2.Address = new Address
            {
                Id = 1,
                LoanId = 1234567,
                IsPrimaryOccupancy = true,
                State = "VA"
            };

            result.Add(loan1);
            result.Add(loan2);

            return result;
        }
    }
}
