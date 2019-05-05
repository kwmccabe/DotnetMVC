using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Models
{
    // select values
    public enum ItemType
    {
        Item,
        Template,
        Content
    }

    public class Item
    {
        public Item()
        {
            ItemType = "Item";
            ItemUsers = new List<ItemUser>();
        }

        public int Id { get; set; }

        [Display(Name = "Item Type"), Required]
        public string ItemType { get; set; }

        [Display(Name = "Keyname"),
            Column(TypeName = "varchar(63)"),
            Remote(action: "VerifyKeyname", controller: "Item", AdditionalFields = nameof(Id)),
            StringLength(63, MinimumLength = 4),
            Required
            ]
        public string Keyname { get; set; }

        [Display(Name = "Created"), DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Modified"), DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }

        public string OwnerId { get; set; }
        public virtual AppUser Owner { get; set; }

        [Display(Name = "Item Users")]
        public virtual ICollection<ItemUser> ItemUsers { get; set; }

    }

    // @see https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/create-a-rest-api-with-attribute-routing
    // DTO = Data Transfer Object 
    public class ItemDTO
    {
        public int Id { get; set; }
        public string ItemType { get; set; }
        public string Keyname { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string OwnerName { get; set; }
    }
}
