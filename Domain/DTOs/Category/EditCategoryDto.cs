using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Category
{
    public class EditCategoryDto
    {
        public string NameE { get; set; }

        public string NameA { get; set; }

        public IFormFile Image { get; set; }

        public bool IsActive { get; set; }
    }
}

