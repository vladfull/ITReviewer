using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITReviewer.Libraries.Models
{
	public class Company
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Вкажіть назву")]
		[MinLength(1,ErrorMessage = "Мінімум 2 символи")]
		[DisplayName("Назва")]
		public string Name { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime RegDate { get; set; }

		[DisplayName("Дата заснування")]
		[Required(ErrorMessage = "Вкажіть дату заснування")]
		public string DateOfFoundation { get; set; }

		[DisplayName("Рейтинг")]
        public double? Rating { get; set; }

        [DisplayName("Опис")]
		public string? Description { get; set; }

	}
}
