using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrders.Models.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        public string NameNormalized { get; set; }

        [Display(Name = "Код")]

        public string Code { get; set; }

        [Display(Name = "Уникален идендификатор")]

        public Guid Identifier { get; set; }

        [Display(Name = "Описание")]

        public string Descriprion { get; set; }

        [Display(Name = "Деактивирана")]

        public bool Disabled { get; set; }

        public int? ParentId { get; set; }

        [Display(Name = "Родителска категория")]

        public string ParentName { get; set; }

        public int? GroupId { get; set; }

        [Display(Name = "Време на създаване UTC")]

        public DateTime CreatedAtUtc { get; set; }

        [Display(Name = "Време на последна редакция UTC")]

        public DateTime? LastModifiedAtUtc { get; set; }

        public string LastModifiedId { get; set; }

        [Display(Name = "Последно редакция от")]

        public string LastModifiedBy { get; set; }
    }
}
