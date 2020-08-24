using System;
using System.Collections.Generic;

namespace AdvantageWeb.Models
{
    public partial class DjangoContentType
    {
        public DjangoContentType()
        {
            AuthPermission = new HashSet<AuthPermission>();
            DjangoAdminLog = new HashSet<DjangoAdminLog>();
        }

        public int Id { get; set; }
        public string AppLabel { get; set; }
        public string Model { get; set; }

        public virtual ICollection<AuthPermission> AuthPermission { get; set; }
        public virtual ICollection<DjangoAdminLog> DjangoAdminLog { get; set; }
    }
}
