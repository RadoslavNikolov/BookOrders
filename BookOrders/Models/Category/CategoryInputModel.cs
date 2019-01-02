using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrders.Models.Category
{
    public class CategoryInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "'{0}' е задължително.")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [StringLength(400, MinimumLength = 3, ErrorMessage = "'{0}' трябва да е не по-малко от {2} и не повече от {1} букви или цифри.")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Descriprion")]
        public string Descriprion { get; set; }

        [Display(Name = "Parent category")]
        public int? ParentId { get; set; }

        public string NameNormmalized => Name != null ? Name.Trim().ToUpper() : Name;
    }
}
