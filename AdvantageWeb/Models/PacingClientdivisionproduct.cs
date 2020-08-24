using System;
using System.Collections.Generic;

namespace AdvantageWeb.Models
{
    public partial class PacingClientdivisionproduct
    {
        public int Id { get; set; }
        public string ClientDivisionProduct { get; set; }
        public string Client { get; set; }

        public virtual PacingClient ClientNavigation { get; set; }
    }
}
