using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJobsPersistent.Models;
using TechJobsPersistent.Data;
using Microsoft.EntityFrameworkCore;
using TechJobsPersistent.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        public string Name { get; set; }
        public int EmployerId { get; set; }
        public List<Employer> SelectListItem { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Skill> PossibleSkills { get; set; }
        public AddJobViewModel()
        {

        }
    }
}
