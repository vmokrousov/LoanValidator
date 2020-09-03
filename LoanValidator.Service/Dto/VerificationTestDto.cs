using LoanValidator.Service.Handling;
using System.Collections.Generic;

namespace LoanValidator.Service.Dto
{
    public class VerificationTestDto
    {
        public VerificationTestDto()
        {
            Errors = new List<VerificationErrorDto>();
        }
        /// <summary>
        /// Verification test object constructor
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        public VerificationTestDto(string name, LoanVerificationResultCode status, List<VerificationErrorDto> errors = null)
        {
            Name = name;
            Status = status;
            Errors = errors;
        }

        /// <summary>
        /// Test name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Status code
        /// </summary>
        public LoanVerificationResultCode Status { get; set; }

        /// <summary>
        /// Collection of verifiction errors
        /// </summary>
        public List<VerificationErrorDto> Errors { get; }
    }
}
