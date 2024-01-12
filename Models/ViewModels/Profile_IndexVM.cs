using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ITReviewer.Libraries.Models.ViewModels
{
    public class Profile_IndexVM
    {
        public string Name { get; set; }

        public string Role { get; set; }

        public string Bio { get; set; }

        public string RegDate { get; set; }

		public string? ImagePath { get; set; }

        public PaginatedList<Review> Reviews { get; set; }
	} 
}
