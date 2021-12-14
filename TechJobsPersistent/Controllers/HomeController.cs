﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;


        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;

        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob(AddJobViewModel addJobViewModel)
        {

            addJobViewModel.SelectListItem = context.Employers.ToList();
            addJobViewModel.PossibleSkills = context.Skills.ToList();

            return View(addJobViewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Job job = new Job(addJobViewModel.Name, addJobViewModel.EmployerId);
                context.Jobs.Add(job);


                context.SaveChanges();
                int i = 0;
                List<Job> list = context.Jobs.Include(j => j.Employer).ToList();

                List<Job> job1 = context.Jobs.Where(j => j.Name == job.Name).Where(j => j.EmployerId == job.EmployerId).Include(j => j.Employer).ToList();

                foreach (Job item in list)
                {
                    if (item == job)
                    {
                        i = item.Id;
                        break;
                    }
                }

                foreach (string skill in selectedSkills)
                {

                    JobSkill newSkill = new JobSkill(job.Id, int.Parse(skill));

                    context.JobSkills.Add(newSkill);


                }

                context.SaveChanges();

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("techjobspersistent@gmail.com", "LaunchCode75?"),
                    EnableSsl = true,
                };

    
                return Redirect("/Home/");
            }

            return View("AddJob", addJobViewModel);



        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}