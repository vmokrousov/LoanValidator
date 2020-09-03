using LoanValidator.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanValidator.Entities
{
    /// <summary>
    /// Describes Loan address. 
    /// See <see cref="Entity"/>.
    /// </summary>
    public class Address : Entity
    {
        /// <summary>
        /// Loan identifier.
        /// </summary>
        [ForeignKey(nameof(Loan))]
        public int LoanId { get; set; }
        /// <summary>
        ///  Primary Occupancy
        /// </summary>
        public bool IsPrimaryOccupancy { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
