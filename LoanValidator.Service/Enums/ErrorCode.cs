using System;
using System.Collections.Generic;
using System.Text;

namespace LoanValidator.Service.Enums
{
    /// <summary>
    /// Specifies possible error codes
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Field is required
        /// </summary>
        Required = 10270001,

        /// <summary>
        /// Input value is not in a valid range
        /// </summary>
        NotInRange = 10450001,

        /// <summary>
        /// Loan ARP rate is not valid.
        /// </summary>
        LoanArpRateIsInvalid = 17000100,

        /// <summary>
        /// Loan Total Fees is not valid.
        /// </summary>
        LoanTotalFeesInvalid = 17000101
    }
}
