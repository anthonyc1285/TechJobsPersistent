using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage = "Name cannot be blank")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location cannot be blank")]

        public string Location { get; set; }
        public Employer Employer { get; set; }
        public List<SelectListItem> Employers { get; set; }

        public AddEmployerViewModel()
        {
        }

    }
}
