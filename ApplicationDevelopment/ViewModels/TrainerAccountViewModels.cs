using ApplicationDevelopment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationDevelopment.ViewModels
{
    public class TrainerAccountViewModels
    {
        public RegisterViewModel RegisterViewModels { get; set; }

        public Trainer Trainers { get; set; }

        public ApplicationUser TrainerUsers { get; set; }
    }
}