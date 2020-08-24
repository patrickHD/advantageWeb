using System;
using System.Collections.Generic;

namespace AdvantageWeb.Models
{
    public partial class AuthUserGroups
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public virtual AuthGroup Group { get; set; }
        public virtual AuthUser User { get; set; }
    }
}
