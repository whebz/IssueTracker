using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Model
{
    public class Account
    {
        public string AccountId { get; set; }

        protected string Password { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// FK (AccountType Entity).
        /// </summary>
        public string AccountTypeId { get; set; }

        public bool Suspended { get; set; }
    }
}
