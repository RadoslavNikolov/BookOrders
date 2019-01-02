using BookOrders.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrders.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameNormalized { get; set; }

        public string Code { get; set; }

        public Guid Identifier { get; set; }

        public string Descriprion { get; set; }

        public bool Disabled { get; set; }

        public int? ParentId { get; set; }

        public int? GroupId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }

        public string LastModifiedId { get; set; }

        [ForeignKey("LastModifiedId")]
        public virtual BookOrdersUser LastModifier { get; set; }

        [ForeignKey("ParentId")]
        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children { get; set; }
    }
}
