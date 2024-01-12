using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITReviewer.Libraries.Models.ViewModels
{
    public class Profile_EditVM
    {
        [MaxLength(100, ErrorMessage = "Максимальний обсяг - 100 символів")]
        [DisplayName("Статус")]
        public string? Bio { get; set; }


        [NotMapped]
        [DisplayName("Фото профілю")]
        public IFormFile? ImageFile { get; set; }

        public string? ImagePath { get; set; }
    }
}
