using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITReviewer.Libraries.Models.ViewModels
{
	public class LoginVM
	{
		[Required(ErrorMessage = "Введіть Логін")]
		[MinLength(5, ErrorMessage = "Мінімальна довжина логіна - 5 символів")]
		[Display(Name = "Логін")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Введіть Пароль")]
		[MinLength(5, ErrorMessage = "Мінімальна довжина логіна - 5 символів")]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		public bool KeepLogged { get; set; }
	}
}
