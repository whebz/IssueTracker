using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Model.Base
{
    public class IDSModel
    {
        /// <summary>
        /// Entity PK
        /// </summary>
        public string Id { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Tell when an entity is suspended.
        /// </summary>
        public bool Suspended { get; set; }
    }
}
