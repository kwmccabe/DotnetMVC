using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Models
{

    public enum ItemUserRole
    {
        User,
        Editor,
        Admin,
    }

    public class ItemUser
    {

        public int ItemId { get; set; }
        [Display(Name = "User Items")]
        public virtual Item Item { get; set; }

        public string UserId { get; set; }
        [Display(Name = "Item Users")]
        public virtual AppUser User { get; set; }

        [Display(Name = "User Access"), Required]
        public ItemUserRole Role { get; set; }

    }

}
