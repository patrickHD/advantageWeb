using System;
using System.Collections.Generic;

namespace AdvantageWeb.Models
{
    public partial class AuthGroup
    {
        public AuthGroup()
        {
            AuthGroupPermissions = new HashSet<AuthGroupPermissions>();
            AuthUserGroups = new HashSet<AuthUserGroups>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AuthGroupPermissions> AuthGroupPermissions { get; set; }
        public virtual ICollection<AuthUserGroups> AuthUserGroups { get; set; }
    }
}
