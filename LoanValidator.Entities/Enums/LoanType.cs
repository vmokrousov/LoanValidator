

using System.ComponentModel.DataAnnotations;

namespace LoanValidator.Entities.Enums
{
    /// <summary>
    /// Loan types
    /// </summary>
    public enum LoanType
    {
        /// <summary>
        /// Conventional
        /// </summary>
        [Display(Name = "Conventional Loan")]
        Conventional = 1,
        /// <summary>
        /// FHA
        /// </summary>
        [Display(Name = "FHA")]
        FHA = 2,
        /// <summary>
        ///  VA (all loan types)
        /// </summary>
        [Display(Name = "VA")]
        VA = 3,
    }
}
