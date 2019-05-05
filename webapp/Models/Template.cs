using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Models
{
    // select values
    public enum TemplateStatus
    {
        Private, 
        Public,
        Hidden,
    }

    // Item-to-Template relation
    // uses Item.ItemType = "Template" as discriminator
    // adds Item.TemplateId and Template.FK_Template_Item_TemplateId
    public class TemplateItem : Item
    {
        public TemplateItem()
        {
            ItemType = "Template";
        }

        [ForeignKey("Id")]
        public virtual Template Template { get; set; }
    }

    // Template-specific fields
    public class Template
    {
        public Template()
        {
            TemplateStatus = "Private";
        }

        public int Id { get; set; }

        [Display(Name = "Template"),
            Column(TypeName = "varchar(63)"),
            Remote(action: "VerifyKeyname", controller: "Item", AdditionalFields = nameof(Id)),
            StringLength(63, MinimumLength = 4),
            Required
            ]
        public string Keyname { get; set; }

        [Display(Name = "Template Title"), Column(TypeName = "varchar(255)"), StringLength(255)]
        public string Title { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Status"), Required]
        public string TemplateStatus { get; set; }

        // link to parent
        public virtual TemplateItem Item { get; set; }

        // link to children
        public virtual ICollection<Content> Content { get; set; }

    }

    // DTO = Data Transfer Object 
    public class TemplateDTO
    {
        public int Id { get; set; }
        public string Keyname { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string TemplateStatus { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string OwnerName { get; set; }
    }
}
