using ApplicationDevelopment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationDevelopment.ViewModels
{
    public class StaffAccountViewModels
    {
        public RegisterViewModel RegisterViewModels { get; set; }

        public Staff Staffs { get; set; }

        public ApplicationUser StaffUsers { get; set; }
    }
}