using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class CreateEditUserRequest
    {
        [Required]
        [MinLength(3)]
        public string Login { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(3)]
        public string Firstname { get; set; }

        [Required]
        [MinLength(3)]
        public string Middlename { get; set; }
        public string Lastname { get; set; }
    }
}