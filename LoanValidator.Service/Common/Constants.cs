

namespace LoanValidator.Service.Common
{
    /// <summary>
    /// App constants
    /// </summary>
    public class Constants
    {
        public class Fee
        {
            public const string Application = "Application";
            public const string Processing = "Processing";
            public const string Settlement = "Settlement";
            public const string FloodCertification = "Flood Certification";
            public const string TitleSearch = "Title Search";
            public const string CreditReport = "Credit Report";
        }
        /// <summary>
        /// Error payload information types
        /// </summary>
        public class ErrorPayloadType
        {
            /// <summary>
            /// Common information
            /// </summary>
            public class Loan
            {
                /// <summary>
                /// Loan Arp Rate Is Invalid Message
                /// </summary>
                public const string LoanArpRateIsInvalidMsg = "A mortgage's APR must not exceed rates specified by the states in which the property is located.";

                /// <summary>
                /// Loan Arp Rate Is Invalid Message
                /// </summary>
                public const string LoanTotalFeesInvalidMsg = "The total fees charged for processing a mortgage cannot exceed the amount specified by each states in which the property is located.";
            }
        }
    }
}
