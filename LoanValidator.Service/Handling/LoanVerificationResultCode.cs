using System;


namespace LoanValidator.Service.Handling
{
    /// <summary>
    /// Defines a set of possible loan verification output
    /// </summary>
    [Flags]
    public enum LoanVerificationResultCode
    {
        /// <summary>
        /// Verification passed succesfully.
        /// </summary>
        Success = 0,

        /// <summary>
        /// A mortgage's APR must not exceed rates specified by the states in which the property is located.
        /// </summary>
        ArpIsExceedRates = 1,

        /// <summary>
        /// The total fees charged for processing a mortgage cannot exceed the amount specified by each state.
        /// </summary>
        TotalFeesExceedTheAmount = 2,
    }
}
