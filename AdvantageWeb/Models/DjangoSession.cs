using System;
using System.Collections.Generic;

namespace AdvantageWeb.Models
{
    public partial class DjangoSession
    {
        public string SessionKey { get; set; }
        public string SessionData { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
