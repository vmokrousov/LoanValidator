using System.Collections.Generic;


namespace LoanValidator.Service.Dto
{
    /// <summary>
    /// Basic verification result data transfer object
    /// </summary>
    public class VerificationResultDto
    {
        /// <summary>
        /// Verifiction result default constructor
        /// </summary>
        public VerificationResultDto()
        {
            Tests = new List<VerificationTestDto>();
        }

        /// <summary>
        /// Collection of verifiction errors
        /// </summary>
        public List<VerificationTestDto> Tests { get; } 
    }
}
