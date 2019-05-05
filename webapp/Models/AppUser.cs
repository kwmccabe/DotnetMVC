using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Models
{

    public class AppUser : IdentityUser
    {

        [PersonalData]
        public string UserNotes { get; set; }

        public virtual ICollection<AppUserClaim> Claims { get; set; }
        public virtual ICollection<AppUserLogin> Logins { get; set; }
        public virtual ICollection<AppUserToken> Tokens { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }

        public virtual ICollection<ItemUser> ItemUsers { get; set; }
    }

    public class AppRole : IdentityRole
    {
        public AppRole() : base()
        {
        }

        public AppRole(string roleName) : base(roleName)
        {
        }

        public string RoleNotes { get; set; }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
    }

    public class AppUserRole : IdentityUserRole<string>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }

    public class AppUserClaim : IdentityUserClaim<string>
    {
        public virtual AppUser User { get; set; }
    }

    public class AppUserLogin : IdentityUserLogin<string>
    {
        public virtual AppUser User { get; set; }
    }

    public class AppRoleClaim : IdentityRoleClaim<string>
    {
        public virtual AppRole Role { get; set; }
    }

    public class AppUserToken : IdentityUserToken<string>
    {
        public virtual AppUser User { get; set; }
    }


}
