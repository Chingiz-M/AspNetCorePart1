using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.ViewModels
{
    public class EmployeeViewModel //: IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Русский или латинский алфавиты с заглавной")]
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя не указано")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина от 2 до 100")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string Lastname { get; set; }

        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Русский или латинский алфавиты с заглавной")]
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия не указана")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина от 2 до 100")]
        public string Surname { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст не указан")]
        [Range(18,90,ErrorMessage = "Возраст от 18 до 90 лет")]
        public int Age { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (validationContext.MemberName == nameof(Age))
        //        return new[] { new ValidationResult("asdas", new[] { nameof(Age) }) };
        //    //throw new System.NotImplementedException();
        //}
    }
}
