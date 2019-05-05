using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Models
{
    // select values
    public enum ContentStatus
    {
        Draft,
        Complete,
        Approved,
        Hidden,
    }

    // Item-to-Content relation
    // uses Item.ItemType = "Content" as discriminator
    // adds Item.ContentId and Content.FK_Content_Item_ContentId
    public class ContentItem : Item
    {
        public ContentItem()
        {
            ItemType = "Content";
        }

        [ForeignKey("Id")]
        public virtual Content Content { get; set; }
    }

    // Content-specific fields
    public class Content
    {
        public Content()
        {
            ContentStatus = "Draft";
        }

        public int Id { get; set; }

        [Display(Name = "Content"),
            Column(TypeName = "varchar(63)"),
            Remote(action: "VerifyKeyname", controller: "Item", AdditionalFields = nameof(Id)),
            StringLength(63, MinimumLength = 4),
            Required
            ]
        public string Keyname { get; set; }

        [Display(Name = "Content Title"), Column(TypeName = "varchar(255)"), StringLength(255)]
        public string Title { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Status"), Required]
        public string ContentStatus { get; set; }

        // link to template
        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

        // link to parent
        public virtual ContentItem Item { get; set; }

    }

    // DTO = Data Transfer Object 
    public class ContentDTO
    {
        public int Id { get; set; }
        public string Keyname { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ContentStatus { get; set; }
        public int TemplateId { get; set; }
        public string TemplateKeyname { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string OwnerName { get; set; }
    }

}
