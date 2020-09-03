using LoanValidator.Entities.BaseEntities;
using LoanValidator.Entities.Enums;
using System.Collections.Generic;

namespace LoanValidator.Entities
{
    /// <summary>
    /// Describes loan. 
    /// See <see cref="Entity"/>.
    /// </summary>
    public class Loan : Entity
    {
        public int ARP { get; set; }
        public int Fee { get; set; }
        public LoanType LoanType { get; set; }

        public double LoanAmount { get; set; }

        public string[] FeesIncludedInTotal { get; set; }
        /// <summary>
        /// Address reference.
        /// See a <see cref="Address"/> class.
        /// </summary>
        public virtual Address Address { get; set; }
    }
}
