using LoanValidator.Service.Enums;


namespace LoanValidator.Service.Dto
{
    /// <summary>
    /// Representation of basic verification error data transfer object
    /// </summary>
    public class VerificationErrorDto
    {
        /// <summary>
        /// Error object constructor
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        public VerificationErrorDto(ErrorCode errorCode, string message)
        {
            ErrorCode = (int)errorCode;
            Message = message;
        }

        /// <summary>
        /// Error code
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }
}
