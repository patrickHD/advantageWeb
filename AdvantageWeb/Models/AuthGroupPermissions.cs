using System;
using System.Collections.Generic;

namespace AdvantageWeb.Models
{
    public partial class AuthGroupPermissions
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PermissionId { get; set; }

        public virtual AuthGroup Group { get; set; }
        public virtual AuthPermission Permission { get; set; }
    }
}
