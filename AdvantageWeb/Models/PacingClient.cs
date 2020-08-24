using System;
using System.Collections.Generic;

namespace AdvantageWeb.Models
{
    public partial class PacingClient
    {
        public PacingClient()
        {
            PacingClientdivisionproduct = new HashSet<PacingClientdivisionproduct>();
            PacingEntry = new HashSet<PacingEntry>();
        }

        public string Client { get; set; }

        public virtual ICollection<PacingClientdivisionproduct> PacingClientdivisionproduct { get; set; }
        public virtual ICollection<PacingEntry> PacingEntry { get; set; }
    }
}
